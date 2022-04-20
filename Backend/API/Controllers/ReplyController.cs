using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class ReplyController : BaseApiController
  {
    private readonly IReplyRepository _replyRepository;
    private readonly IThreadRepository _threadRepository;
    private readonly IMapper _mapper;
    private readonly IAppService _appService;

    public ReplyController(
        IReplyRepository replyRepository, 
        IThreadRepository threadRepository, 
        IMapper mapper, 
        IAppService appService)
    {
      _replyRepository = replyRepository;
      _threadRepository = threadRepository;
      _mapper = mapper;
      _appService = appService;
    }

    [HttpGet("{replyId}")]
    [Authorize]
    public async Task<ActionResult<UserReply>> getReplyById(string replyId)
    {
      return await _replyRepository.GetReplyByIdAsync(Int32.Parse(replyId));
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
        Replies = newThread.Replies,
        Views = newThread.Views,
        UserName = newThread.User.UserName,
        Email = newThread.User.Email,
        WasCreated = newThread.WasCreated
      };

      return Ok(returnThread);
    }

    [Route("category/{categoryId}")]
    [HttpGet]
    // [Authorize]
    public async Task<IEnumerable<UserThread>> getThreadsByCategoryId(string categoryId)
    {
      var hej = categoryId;

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