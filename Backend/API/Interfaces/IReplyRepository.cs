using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface IReplyRepository
  {
    Task<UserReply> GetReplyByIdAsync(int id);
    Task<IEnumerable<UserReply>> GetRepliesFromUserAsync(AppUser user);
    Task<IEnumerable<UserReply>> GetRepliesByThreadIdAsync(string threadId);
    void Update(ReplyDto replyDto); //Update kommer bara uppdatera Tracking status i Entity Frameworket.. för att säga att någonting har förändrats.. men alla andra är Tasks!
    Task<bool> SaveAllAsync();
    void Add(UserReply userReply);
  }
}