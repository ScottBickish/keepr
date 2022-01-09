using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsRepository _vrepo;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vrepo)
    {
      _repo = repo;
      _vrepo = vrepo;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep, Profile userInfo)
    {
      if (newVaultKeep.CreatorId != userInfo?.Id)
      {
        throw new Exception("er wrong-O");
      }
      return _repo.Create(newVaultKeep);
    }

    internal List<VKVM> GetKeepsByVaultId(int id, Profile userInfo)
    {

      Vault vault = _vrepo.GetVaultById(id);
      if (vault == null)
      {
        throw new Exception("Invalid Vault Id...");
      }
      else if (vault.CreatorId == userInfo?.Id || !vault.IsPrivate)
      {
        List<VKVM> vk = _repo.GetKeepsByVaultId(id);
        if (vk == null)
        {
          throw new Exception("Invalid Id man!!!");
        }
        return vk;
      }
      throw new Exception("this is private");
      // List<VaultKeep> found = _repo.GetKeepsByVaultId(id);
      // if (found == null)
      // {
      //   throw new Exception("Invalid Vault Id VkS");
      // }
      // return found;
    }

    internal VaultKeep GetVaultKeepById(int id)
    {
      VaultKeep found = _repo.GetVaultKeepById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id VkS");
      }
      return found;
    }

    internal void Delete(int id, Profile userInfo)
    {
      VaultKeep vaultKeep = GetVaultKeepById(id);
      if (vaultKeep.CreatorId != userInfo.Id)
      {
        throw new Exception("You cannot delete this");
      }
      _repo.Delete(id);
    }
  }
}
