using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
  public interface IThreadRepository
  {
    Task<UserThread> GetThreadByIdAsync(int id);
    Task<PagedList<UserThread>> GetLatestThreads(UserParams userParams, int amount);
    Task<IEnumerable<UserThread>> GetThreadsFromUserAsync(AppUser user);
    Task<PagedList<UserThread>> GetThreadsByCategoryIdAsync(UserParams userParams, string categoryId);
    void DeleteThreadById(UserThread userThread);
    void Add(UserThread thread);
    void Update(UserThread thread); //Update kommer bara uppdatera Tracking status i Entity Frameworket.. för att säga att någonting har förändrats.. men alla andra är Tasks!
    Task<bool> SaveAllAsync();
  }
}