using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps(creatorId, name, description, img, views, shares, keeps)
      VALUES (@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    internal Keep GetKeepById(int id)
    {
      string sql = @"
      UPDATE keeps
      SET
      views = views +1
      WHERE id = @id;
      SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.id = @id
      ;";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).FirstOrDefault();
    }

    internal object GetAllKeeps()
    {
      string sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      
      ;";
      return _db.Query<Keep, Profile, Keep>(sql,
      (k, a) =>
      {
        k.Creator = a;
        return k;
      }, splitOn: "id").ToList();
    }

    internal List<Keep> GetUsersKeeps(string id)
    {
      string sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @id
      ;";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).ToList();
    }

    internal Keep Edit(Keep updatedKeep)
    {
      string sql = @" 
      UPDATE keeps
      SET
      creatorId = @CreatorId,
      name = @Name,
      description = @Description,
      img = @Img
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, updatedKeep);
      if (rows <= 0)
      {
        throw new Exception("Keep was not updated Repository");
      }
      return updatedKeep;

    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM keeps WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}