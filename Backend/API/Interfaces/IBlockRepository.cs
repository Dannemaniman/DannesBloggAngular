using API.Entities;

namespace API.Interfaces
{
  public interface IBlockRepository
  {
    Task<UserBlockList> BlockUser(AppUser user, DateTime duration);
    void Add(AppUser user);
    void Update(AppUser user); //Update kommer bara uppdatera Tracking status i Entity Frameworket.. för att säga att någonting har förändrats.. men alla andra är Tasks!
    Task<bool> SaveAllAsync();
  }
  
}