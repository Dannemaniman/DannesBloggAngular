using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
  public interface IReplyRepository
  {
    Task<UserReply> GetReplyByIdAsync(int id);
    Task<IEnumerable<UserReply>> GetRepliesFromUserAsync(AppUser user);
    Task<PagedList<UserReply>> GetRepliesByThreadIdAsync(UserParams userParams, string threadId);
    void Add(UserReply userReply);
    void DeleteReplyById(UserReply userReply);
    void Update(UserReply replyDto); //Update kommer bara uppdatera Tracking status i Entity Frameworket.. för att säga att någonting har förändrats.. men alla andra är Tasks!
    Task<bool> SaveAllAsync();
  }
}