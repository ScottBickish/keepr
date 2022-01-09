using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    internal Profile GetByProfileId(string id)
    {
      Profile Pro = _repo.GetByProfileId(id);
      if (Pro == null)
      {
        throw new Exception("Invalid Pro Id Bro");
      }
      return Pro;
    }
  }
}
