using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class ThreadRepository : IThreadRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public ThreadRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }
    public async Task<UserThread> GetThreadByIdAsync(int id)
    {
       var thread = await _context.UserThreads.Include(p => p.User).Include(p => p.Replies).FirstOrDefaultAsync(p => p.Id == id);
      //  thread.Created = DateTime.UtcNow;
      //  return _mapper.Map<ReturnThread>(thread);
      return thread;
    }

    public async Task<IEnumerable<UserThread>> GetLatestThreads(int amount)
    {
            //Denna query kan jag använda om jag vill https://stackoverflow.com/questions/11903269/return-entity-with-latest-timestamp-matching-a-userid-linq
      // var act = _context.ActivityLogs
      //   .Where(a => a.UserId == user.UserId)
      //   .OrderByDescending(a => a.Timestamp)
      //   .FirstOrDefault();
        //Eller denna Mytable.OrderByDesc(x => x.TimesRead).take(5); https://stackoverflow.com/questions/19889461/how-to-get-the-5-highest-values-trough-linq
        var threads = await _context.UserThreads.OrderByDescending(x => x.WasCreated).Take(amount).Include(p => p.Replies).ToListAsync();

        return threads;
    }

    public async Task<IEnumerable<UserThread>> GetThreadsFromUserAsync(AppUser user)
    {
      return await _context.UserThreads
        .Where<UserThread>(thread => thread.User == user)
        .ToListAsync();
    }

    public async Task<IEnumerable<UserThread>> GetThreadsByCategoryIdAsync(string categoryId)
    {
    return await _context.UserThreads
        .Where<UserThread>(thread => thread.CategoryId == categoryId)
        .ToListAsync();
    }
    public void Update(ThreadDto thread) //denna är annorlunda.. vi kommer inte förändra ngt i databasen.. men vi kommer markera den, att den har blivit modifierad..
    {
      //Vi sätter .Statet av entityn.. till .modified.. detta låter Entity Frameworket uppdatera och lägga till en flag.. som säger att den har blivit modifierad!:
      _context.Entry(thread).State = EntityState.Modified;
    }
    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0; //returnar +1 för varje förändring till databasen
    }
    public void Add(UserThread thread)
    {
        _context.Add(thread); 
    }

    // public async Task<MemberDto> GetMemberAsync(string username)
    // {
    //         return await _context.Users
    //   .Where(x => x.UserName == username)
    //   .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
    //   .SingleOrDefaultAsync();
    //   //—- NOTERA ATT MED PROJECTION .ProjectTo() SÅ BEHÖVER VI INTE ANVÄNDA .Include() PGA ENTITY FRAMEWORKET KOMMER FIXA FRAM RÄTT QUERY.. 
    //   //FÖR ATT JOINA TABLET OCH FÅ DET VI BEHÖVER!!
    //   //SÅ DETTA KAN VARA ETT MER EFFICIENT SÄTT ATT GÖRA SAKER!!!
    // } 
    }
}