using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults(creatorId, name, description, isPrivate)
      VALUES (@CreatorId, @Name, @Description, @IsPrivate);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newVault);
      newVault.Id = id;
      return newVault;
    }

    internal Vault GetVaultById(int id)
    {
      string sql = @"
      SELECT 
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id = @id
      ;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).FirstOrDefault();
    }

    internal Vault Edit(Vault updatedvault)
    {
      string sql = @" 
      UPDATE vaults
      SET
      creatorId = @CreatorId,
      name = @Name,
      description = @Description,
      isPrivate = @IsPrivate
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, updatedvault);
      if (rows <= 0)
      {
        throw new Exception("Vault was not updated Repository");
      }
      return updatedvault;
    }

    // internal List<Vault> GetMyVaults(string id)
    // {

    // }

    internal List<Vault> GetUsersVaults(string id)
    {
      string sql = @"
      SELECT 
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId = @id
      ;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).ToList();
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM vaults WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}