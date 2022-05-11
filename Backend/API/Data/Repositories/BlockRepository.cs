using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
  public class BlockRepository : IBlockRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public BlockRepository(DataContext context, UserManager<AppUser> userManager, IMapper mapper)
    {
      _userManager = userManager;
      _mapper = mapper;
      _context = context;

    }

    public async Task<UserBlockList> IsUserBlocked(int userId)
    {
      return await _context.UserBlockList
        .Where(item => item.UserId == userId)
        .FirstOrDefaultAsync();
    }

    public void Update(UserBlockList userBlockList)
    {
      _context.Entry(userBlockList).State = EntityState.Modified;
    }

    public void Add(UserBlockList userBlockList)
    {
      _context.Add(userBlockList);
    }

    public void DeleteBlock(UserBlockList userBlockList)
    {
       _context.Entry(userBlockList).State = EntityState.Deleted;
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0; 
    }
  }
}