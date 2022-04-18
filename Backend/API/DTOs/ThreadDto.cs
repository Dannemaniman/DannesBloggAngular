using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
  public class ThreadDto
  {
    public int Id { get; set; }
    public int Replies { get; set; }
    public int Views { get; set; }
    public string? Content { get; set; }
    public DateTime? Created { get; set; }
  }
}