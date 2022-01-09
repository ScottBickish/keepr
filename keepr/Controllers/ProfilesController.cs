using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
    {
      _ps = ps;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetUsersVaults(string id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        List<Vault> v = _vs.GetUsersVaults(id, userInfo);
        return Ok(v);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }


    [HttpGet("{id}")]
    public ActionResult<Profile> GetByProfileId(string id)
    {
      try
      {
        Profile pro = _ps.GetByProfileId(id);
        return Ok(pro);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetUsersKeeps(string id)
    {
      try
      {
        List<Keep> keep = _ks.GetUsersKeeps(id);
        return Ok(keep);
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }

  }
}