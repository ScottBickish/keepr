using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _krepo;

    public KeepsService(KeepsRepository krepo)
    {
      _krepo = krepo;
    }
    internal Keep Create(Keep newKeep)
    {
      return _krepo.Create(newKeep);
    }

    internal object GetAllKeeps()
    {
      return _krepo.GetAllKeeps();
    }

    internal Keep GetKeepById(int id)
    {
      Keep found = _krepo.GetKeepById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id KS");
      }
      return found;
    }

    internal Keep Edit(Keep updatedKeep, string userId)
    {
      Keep oldKeep = GetKeepById(updatedKeep.Id);
      if (oldKeep.CreatorId != userId)
      {
        throw new Exception("You cannot edit this");
      }

      updatedKeep.CreatorId = updatedKeep.CreatorId != null ? updatedKeep.CreatorId : oldKeep.CreatorId;
      updatedKeep.Name = updatedKeep.Name != null ? updatedKeep.Name : oldKeep.Name;
      updatedKeep.Description = updatedKeep.Description != null ? updatedKeep.Description : oldKeep.Description;
      updatedKeep.Img = updatedKeep.Img != null ? updatedKeep.Img : oldKeep.Img;
      return _krepo.Edit(updatedKeep);
    }

    internal List<Keep> GetUsersKeeps(string id)
    {
      return _krepo.GetUsersKeeps(id);
    }

    internal void Delete(int id, string userId)
    {
      Keep toDelete = GetKeepById(id);
      if (toDelete.CreatorId != userId)
      {
        throw new Exception("You cannot delete this");
      }
      _krepo.Delete(id);
    }
  }
}