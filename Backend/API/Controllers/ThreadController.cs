using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class ThreadController : BaseApiController
  {
    private readonly IThreadRepository _threadRepository;
    private readonly IMapper _mapper;
    private readonly IAppService _appService;
    private readonly IUserRepository _userRepository;
    public ThreadController(IUserRepository userRepository, IThreadRepository threadRepository, IMapper mapper, IAppService appService)
    {
      _userRepository = userRepository;
      _appService = appService;
      _mapper = mapper;
      _threadRepository = threadRepository;
    }

    [HttpGet("{threadId}")]
    [Authorize]
    public async Task<ActionResult<ReturnThread>> getThreadById(int threadId)
    {
      var thread = await _threadRepository.GetThreadByIdAsync(threadId);

      var returnThread = _mapper.Map<ReturnThread>(thread);

      return returnThread;
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
    public async Task<IEnumerable<UserThread>> getThreadsByCategoryId(string categoryId)
    {
      return await _threadRepository.GetThreadsByCategoryIdAsync(categoryId);
    }

    public async Task<AppUser> GetUser(string username) 
    {
      var user = await _userRepository.GetUserByUsernameAsync(username);

      if(user == null) return null;

      return user;
    }
  }
}