using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }
    [HttpPost]
    [Authorize]
    // FIXME only this function isnt passing in postman it needs to return an error if the user isnt who they say they are
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVaultKeep.CreatorId = userInfo?.Id;
        VaultKeep vaultKeep = _vks.Create(newVaultKeep, userInfo);
        newVaultKeep.Creator = userInfo;
        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }

    }
    // NOTE THIS ID IS THE VAULTKEEP ID
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        _vks.Delete(id, userInfo);
        return Ok("Successfully Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }

    }
  }
}