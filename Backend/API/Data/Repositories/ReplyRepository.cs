

using API.DTOs;
using API.Entities;
using API.Helpers;
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
       var reply = await _context.UserReplies.FindAsync(id);
       return _mapper.Map<UserReply>(reply);
    }

    public async Task<IEnumerable<UserReply>> GetRepliesFromUserAsync(AppUser user)
    {
      return await _context.UserReplies
        .Where<UserReply>(thread => thread.User == user)
        .Include(x => x.Thread)
        .ToListAsync();
    }

    public async Task<PagedList<UserReply>> GetRepliesByThreadIdAsync(UserParams userParams, string threadId)
    {
      var query =  _context.UserReplies
        .Where<UserReply>(thread => thread.Id == Int32.Parse(threadId))
        .AsNoTracking();

      return await PagedList<UserReply>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
      /*VI SER NU ATT TYPEN AV QUERYN OVAN ÄR EN IQueryable! 
        Detta är alltså ett Expression Tree.. som kommer byggas av Entity Frameworket.. Sedan när vi executar .ToListAsync().. 
        Så kommer den executa requesten till vår databas!
        Det vi också kan specifiera i trädet, för att göra det ÄNNU mera optimerat.. är att by default, när vi hämtar entities från Entity ramverket.. så applyar Entity Tracking på dessa entiteterna..
        I med att detta är en lista som vi bara läser från.. så kan vi specifiera .AsNoTracking()! Och då kommer Entity ramverket inte tracka den!: 
        
        “MEN.. JAG EXECUTAR ALDRIG MIN REQUEST JU???..”
        
        Jo det gör vi! Kolla nu igen i CreateAsync metoden!! Vi kör våra 2 requests där!
        Vi skickar alltså detta till en metod som executar det åt oss!*/
    }
    public void Update(UserReply reply) 
    {
      _context.Entry(reply).State = EntityState.Modified;
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