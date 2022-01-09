using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      UPDATE keeps
      SET keeps = keeps + 1
      WHERE id = @KeepId;
      INSERT INTO vaultkeeps(creatorId, vaultId, keepId)
      VALUES (@CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }

    // NOTE  id here is the vault id
    internal List<VKVM> GetKeepsByVaultId(int id)
    {
      string sql = @"
      SELECT 
      k.*,
      vk.id AS vaultKeepId,
      a.*
      FROM vaultkeeps vk
      JOIN  keeps k ON k.id = vk.keepId
      JOIN accounts a ON k.creatorId = a.id  
      WHERE vk.vaultId = @id
      ;";
      return _db.Query<VKVM, Profile, VKVM>(sql, (vk, a) =>
      {
        vk.Creator = a;
        return vk;
      }, new { id }, splitOn: "id").ToList();
    }

    internal VaultKeep GetVaultKeepById(int id)
    {
      string sql = @"SELECT * FROM vaultkeeps 
        WHERE id = @id
      ;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaultkeeps
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, new { id });
      if (rows <= 0)
      {
        throw new Exception("Invalid Id");
      }
    }
  }
}