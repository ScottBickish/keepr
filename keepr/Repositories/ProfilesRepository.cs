using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetByProfileId(string id)
    {
      string sql = @"
      SELECT * FROM 
      accounts WHERE id = @id
      ;";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }
  }
}
