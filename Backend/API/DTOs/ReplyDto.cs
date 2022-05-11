using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
  public class ReplyDto
  {
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? ThreadId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public UserThread? Thread { get; set; }
  }
}