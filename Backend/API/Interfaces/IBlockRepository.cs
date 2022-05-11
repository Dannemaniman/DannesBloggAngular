using API.Entities;

namespace API.Interfaces
{
  public interface IBlockRepository
  {
    Task<UserBlockList> IsUserBlocked(int userId);
    void Add(UserBlockList userBlockList);
    void Update(UserBlockList userBlockList); //Update kommer bara uppdatera Tracking status i Entity Frameworket.. för att säga att någonting har förändrats.. men alla andra är Tasks!
    void DeleteBlock(UserBlockList userBlockList);
    Task<bool> SaveAllAsync();
  }

}