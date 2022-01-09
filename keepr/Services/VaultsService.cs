using System;
using System.Collections.Generic;
using System.Linq;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vrepo;

    public VaultsService(VaultsRepository vrepo)
    {
      _vrepo = vrepo;
    }

    internal Vault Create(Vault newVault, Profile userInfo)
    {
      if (newVault.CreatorId != userInfo?.Id)
      {
        throw new Exception("Are you signed in?");
      }
      return _vrepo.Create(newVault);
    }

    internal Vault GetVaultById(int id, Profile userInfo)
    {
      Vault found = _vrepo.GetVaultById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id VS");
      }
      if (found.CreatorId != userInfo?.Id && found.IsPrivate)
      {
        throw new Exception("that aint yours to see");
      }

      return found;

    }

    internal Vault Edit(Vault updatedVault, string userId)
    {
      Vault oldVault = _vrepo.GetVaultById(updatedVault.Id);
      if (oldVault.CreatorId != userId)
      {
        throw new Exception("You cannot edit this");
      }
      else if (oldVault == null)
      {
        throw new Exception("Invalid Id VS");
      }
      updatedVault.CreatorId = updatedVault.CreatorId != null ? updatedVault.CreatorId : oldVault.CreatorId;
      updatedVault.Name = updatedVault.Name != null ? updatedVault.Name : oldVault.Name;
      updatedVault.Description = updatedVault.Description != null ? updatedVault.Description : oldVault.Description;
      updatedVault.IsPrivate = updatedVault.IsPrivate != null ? updatedVault.IsPrivate : oldVault.IsPrivate;
      return _vrepo.Edit(updatedVault);
    }

    internal List<Vault> GetMyVaults(string id)
    {
      List<Vault> v = _vrepo.GetUsersVaults(id);
      return v.ToList();
    }

    internal List<Vault> GetUsersVaults(string id, Profile userInfo)
    {
      List<Vault> v = _vrepo.GetUsersVaults(id);
      if (userInfo == null)
      {
        return v.ToList().FindAll(v => v.IsPrivate == false);

      }
      return v;
    }

    internal void Delete(int id, string userId)
    {
      Vault toDelete = _vrepo.GetVaultById(id);
      if (toDelete.CreatorId != userId)
      {
        throw new Exception("You cannot delete this");
      }
      _vrepo.Delete(id);
    }
  }
}