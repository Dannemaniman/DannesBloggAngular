using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
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
    public async Task<ActionResult<ReturnReply>> createNewReply(ReplyDto replyDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (username == null) return null;

        var user = await GetUser(username);

        if(user == null) return NotFound();

        replyDto.User = user;

        var thread = await _threadRepository.GetThreadByIdAsync(Int32.Parse(replyDto.ThreadId));

        replyDto.Thread = thread;

        var newReply = _mapper.Map<UserReply>(replyDto);

        if(newReply == null) return Problem();

        _replyRepository.Add(newReply);

        if(await _replyRepository.SaveAllAsync() == false) return Problem();

        var returnReply = new ReturnReply
        {
            Id = newReply.Id,
            Title = newReply.Title,
            Content = newReply.Content,
            UserName = newReply.User.UserName,
            Email = newReply.User.Email,
            WasCreated = newReply.WasCreated
        };

        return Ok(returnReply);
    }

    [Route("thread/{threadId}")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserReply>>> getRepliesByThreadId(UserParams userParams, string threadId)
    {
      var replies = await _replyRepository.GetRepliesByThreadIdAsync(userParams, threadId);

      Response.AddPaginationHeader(replies.CurrentPage, replies.PageSize, replies.TotalCount, replies.TotalPages);

      if(replies.Equals(null)) return NotFound();

      return Ok(replies);
    }



    public async Task<AppUser> GetUser(string username) 
    {
      var user = await _userRepository.GetUserByUsernameAsync(username);

      if(user == null) return null;

      return user;
    }
  }
}