using System.Collections;
using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
/*
Viewn kommer komma från Angular! INNAN SÅ KOM Viewn FRÅN dotnet 
PGA I TIDIGARE VERSIONER LÅNGT TILLBAKA SKICKADE MAN TILLBAKA STATISKA SIDOR 
FRÅN WEBBSERVERN.. NU HAR VI EN FRONTEND APP.. SOM ÄR VIEWN!
*/

namespace API.Controllers;

public class UsersController : BaseApiController
{
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;
  public UsersController(IUserRepository userRepository, IMapper mapper)
  {
    _mapper = mapper;
    _userRepository = userRepository;
  }

  [HttpGet]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
  {

    var users = await _userRepository.GetUsersAsync();

    //AutoMapper löser all mapping mellan AppUser och MemberDto!
    // AUTOMAPPER KOMMER VARA SMART NOG ATT KÄNNA IGEN PROPERTIES SOM HAR SAMMA NAMN!:
    var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

    return Ok(usersToReturn);

  }
  [HttpGet("{Id}")]
  [Authorize]
  public async Task<ActionResult<MemberDto>> GetUser(int id)
  {
    var user = await _userRepository.GetUserByIdAsync(id);

    if (user == null) return NoContent();

    return _mapper.Map<MemberDto>(user);
  }
  [HttpPut]
  [Authorize]
  public async Task<ActionResult> UpdateUserPassword(MemberUpdateDto memberUpdateDto)
  {
    
    var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if(username == null) return NotFound();

    var user = await _userRepository.GetUserByUsernameAsync(username);

    //NU ÄR USER FLAGGAT ATT UPPDATERAS AV Entity RAMVERKET!
    //DETTA GARANTERAR ATT.. OAVSETT OM VÅR USER INTE HAR UPPDATERATS.. 
    //SÅ GENOM ATT LÄGGA TILL DENNA FLAGEN i Update.. SÅ GARANTERAR VI ATT VI INTE FÅR TILLBAKA ETT ERROR ELLER ETT EXCEPTION!
     _mapper.Map(memberUpdateDto, user);

     _userRepository.Update(user);

     if(await _userRepository.SaveAllAsync()) return NoContent();

     return BadRequest("Failed to update user.");
  }
}