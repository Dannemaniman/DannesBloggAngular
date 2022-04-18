using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public UserRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }
    public async Task<AppUser> GetUserByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }
    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
      return await _context.Users.SingleOrDefaultAsync(user => user.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
      return await _context.Users.ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0; //returnar +1 för varje förändring till databasen
    }

    public void Update(AppUser user) //denna är annorlunda.. vi kommer inte förändra ngt i databasen.. men vi kommer markera den, att den har blivit modifierad..
    {
      //Vi sätter .Statet av entityn.. till .modified.. detta låter Entity Frameworket uppdatera och lägga till en flag.. som säger att den har blivit modifierad!:
      _context.Entry(user).State = EntityState.Modified;
    }
    public async Task<IEnumerable<MemberDto>> GetMembersAsync(string username)
    {
        return await _context.Users
          .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
          .ToListAsync();
    }
    public async Task<MemberDto> GetMemberAsync(string username)
    {
            return await _context.Users
      .Where(x => x.UserName == username)
      .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
      .SingleOrDefaultAsync();
      //—- NOTERA ATT MED PROJECTION .ProjectTo() SÅ BEHÖVER VI INTE ANVÄNDA .Include() PGA ENTITY FRAMEWORKET KOMMER FIXA FRAM RÄTT QUERY.. 
      //FÖR ATT JOINA TABLET OCH FÅ DET VI BEHÖVER!!
      //SÅ DETTA KAN VARA ETT MER EFFICIENT SÄTT ATT GÖRA SAKER!!!
    }


  }
}