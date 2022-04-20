using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface IThreadRepository
  {
    // Task<IEnumerable<ThreadDto>> GetThreadsAsync();
    Task<ReturnThread> GetThreadByIdAsync(int id);
    Task<IEnumerable<UserThread>> GetThreadFromUserAsync(AppUser user);
    Task<IEnumerable<UserThread>> GetThreadsByCategoryIdAsync(string categoryId);
    void Update(ThreadDto thread); //Update kommer bara uppdatera Tracking status i Entity Frameworket.. för att säga att någonting har förändrats.. men alla andra är Tasks!
    Task<bool> SaveAllAsync();
    void Add(UserThread thread);
    // Task<ThreadDto> SaveThread ();
  }
}