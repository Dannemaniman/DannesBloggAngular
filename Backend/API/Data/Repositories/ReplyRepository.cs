

using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class ReplyRepository : IReplyRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public ReplyRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }
    public async Task<UserReply> GetReplyByIdAsync(int id)
    {
       var reply = await _context.UserThreads.FindAsync(id);
       return _mapper.Map<UserReply>(reply);
    }

    public async Task<IEnumerable<UserReply>> GetRepliesFromUserAsync(AppUser user)
    {
      return await _context.UserReplies
        .Where<UserReply>(thread => thread.User == user)
        .ToListAsync();
    }

    public async Task<IEnumerable<UserReply>> GetRepliesByThreadIdAsync(string threadId)
    {
    return await _context.UserReplies
        .Where<UserReply>(thread => thread.Id == Int32.Parse(threadId))
        .ToListAsync();
    }
    public void Update(ReplyDto replyDto) 
    {
      _context.Entry(replyDto).State = EntityState.Modified;
    }
    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0; //returnar +1 för varje förändring till databasen
    }
    public void Add(UserReply userReply)
    {
        _context.Add(userReply); 
    }
  }
}