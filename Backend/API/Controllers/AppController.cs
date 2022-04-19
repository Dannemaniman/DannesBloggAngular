using API.DTOs;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class AppController : BaseApiController
  {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAppService _appService;
    public AppController(IUserRepository userRepository, IMapper mapper, IAppService appService)
    {
      _appService = appService;
      _mapper = mapper;
      _userRepository = userRepository;
    }

    [HttpGet("categories")]
    [AllowAnonymous]
    public ActionResult<List<Category>> getCategories()
    {
        var jsonData = _appService.DeserializeFromStream();

        if(jsonData == null) return NotFound();

        return Ok(jsonData);
    }
  }
}