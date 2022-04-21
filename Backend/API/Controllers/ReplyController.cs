using System.Security.Claims;
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
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ReplyController(
        IUserRepository userRepository,
        IReplyRepository replyRepository, 
        IThreadRepository threadRepository, 
        IMapper mapper)
    {
      _replyRepository = replyRepository;
      _threadRepository = threadRepository;
      _userRepository = userRepository;
      _mapper = mapper;
    }

    [HttpGet("{replyId}")]
    [Authorize]
    public async Task<ActionResult<UserReply>> getReplyById(string replyId)
    {
      return await _replyRepository.GetReplyByIdAsync(Int32.Parse(replyId));
    }

    [HttpPost()]
    [Authorize]
    public async Task<ActionResult<UserReply>> createNewReply(ReplyDto replyDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (username == null) return null;

        var user = await GetUser(username);

        if(user == null) return NotFound();

        replyDto.User = user;

        var newReply = _mapper.Map<UserReply>(replyDto);

        if(newReply == null) return Problem();

        _replyRepository.Add(newReply);

        if(await _replyRepository.SaveAllAsync() == false) return Problem();

        // var returnThread = new ReturnThread
        // {
        //     Id = newThread.Id,
        //     Title = newThread.Title,
        //     Content = newThread.Content,
        //     CategoryId = newThread.CategoryId,
        //     Replies = newThread.Replies,
        //     Views = newThread.Views,
        //     UserName = newThread.User.UserName,
        //     Email = newThread.User.Email,
        //     WasCreated = newThread.WasCreated
        // };

        return Ok(newReply);
    }

    [Route("thread/{threadId}")]
    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<UserReply>> getRepliesByThreadId(string threadId)
    {
      return await _replyRepository.GetRepliesByThreadIdAsync(threadId);
    }



    public async Task<AppUser> GetUser(string username) 
    {
      var user = await _userRepository.GetUserByUsernameAsync(username);

      if(user == null) return null;

      return user;
    }
  }
}