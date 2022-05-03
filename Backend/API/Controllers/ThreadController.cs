using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class ThreadController : BaseApiController
  {
    private readonly IThreadRepository _threadRepository;
    private readonly IMapper _mapper;
    private readonly IAppService _appService;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<AppUser> _userManager;
    public ThreadController(
      IUserRepository userRepository, 
      IThreadRepository threadRepository, 
      IMapper mapper, 
      UserManager<AppUser> userManager, 
      IAppService appService)
    {
      _userRepository = userRepository;
      _threadRepository = threadRepository;
      _mapper = mapper;
      _userManager = userManager;
      _appService = appService;
    }

    [HttpGet("{threadId}")]
    [Authorize]
    public async Task<ActionResult<ReturnThread>> getThreadById(int threadId)
    {
      var thread = await _threadRepository.GetThreadByIdAsync(threadId);

      if (thread == null) return NotFound();

      return _mapper.Map<ReturnThread>(thread);
    }

    [HttpPut("{threadId}")]
    [Authorize]
    public async Task<ActionResult<ReturnThread>> UpdateThread(string threadId, ThreadUpdateDto threadUpdate)
    {
      var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
      if (username == null) return BadRequest();

      var user = _userManager.Users.SingleOrDefault(x => x.UserName == username);

      if (user == null) return BadRequest();

      var thread = await _threadRepository.GetThreadByIdAsync(Int16.Parse(threadId));

      if (thread == null) return BadRequest();

      if (thread.User.Id != user.Id) return Unauthorized();
      
      var updatedThread = _mapper.Map(threadUpdate, thread);
    
      _threadRepository.Update(updatedThread);

      if(await _threadRepository.SaveAllAsync()) return NoContent();

      return BadRequest("Failed to update.");
    }

    
    [HttpGet("latest")]
    public async Task<ActionResult<IEnumerable<ReturnThread>>> getLatestThreads([FromQuery]UserParams userParams)
    {
      var threads = await _threadRepository.GetLatestThreads(userParams, 10);

      Response.AddPaginationHeader(threads.CurrentPage, threads.PageSize, threads.TotalCount, threads.TotalPages);

      if(threads.Equals(null)) return NotFound();

      return Ok(_mapper.Map<List<ReturnThread>>(threads));
    }

    [HttpPost()]
    [Authorize]
    public async Task<ActionResult<ReturnThread>> createNewThread(ThreadDto threadData)
    {
      var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

      if (username == null) return null;

     var user = await GetUser(username);

     if(user == null) return NotFound();

      threadData.User = user;

      var newThread = _mapper.Map<UserThread>(threadData);

      if(newThread == null) return Problem();

      _threadRepository.Add(newThread);

      if(await _threadRepository.SaveAllAsync() == false) return Problem();

      var returnThread = new ReturnThread
      {
        Id = newThread.Id,
        Title = newThread.Title,
        Content = newThread.Content,
        CategoryId = newThread.CategoryId,
        RepliesCount = newThread.RepliesCount,
        ViewsCount = newThread.ViewsCount,
        UserName = newThread.User.UserName,
        Email = newThread.User.Email,
        Replies = newThread.Replies,
        WasCreated = newThread.WasCreated
      };

      return Ok(returnThread);
    }

    [Route("category/{categoryId}")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserThread>>> getThreadsByCategoryId([FromQuery]UserParams userParams, string categoryId)
    {
      //På grund av att vi är i en ApiController.. Och faktumet att vi har satt det Attributet.. så borde Controllern vara smart nog att 
      //känna igen när vi skickar upp query string parametrar (pagination info från clienten).. 
      //Och den kommer matcha dem till UserParams classen!
      var threads = await _threadRepository.GetThreadsByCategoryIdAsync(userParams, categoryId);

      Response.AddPaginationHeader(threads.CurrentPage, threads.PageSize, threads.TotalCount, threads.TotalPages);

      if(threads.Equals(null)) return NotFound();

      return Ok(threads);
    }

    public async Task<AppUser> GetUser(string username) 
    {
      var user = await _userRepository.GetUserByUsernameAsync(username);

      if(user == null) return null;

      return user;
    }
  }
}