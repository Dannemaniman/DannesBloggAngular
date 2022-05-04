using API.Interfaces;
using AutoMapper;

namespace API.Data.Repositories
{
  public class BlockRepository : IBlockRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public BlockRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;

    }
  }
}