using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  //A DbContext instance represents a session with the database and can be used to query and save instances of your entities. 
  //DbContext is a combination of the Unit Of Work and Repository patterns.
  public class DataContext : DbContext
  {
    //Vi kommer skicka lite options till denna classen när vi lägger till den till Startup Configurationen.. 
    //där vi lägger till den till vår Dependency Injection Container:
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<AppUser> Users { get; set; } //Nästa steg är att lägga till min DbContext till min Startup Configuration start up class.. så vi kan injecta datacontext in i andra delar av min app..!
    public DbSet<UserThread> UserThreads { get; set; } //Nästa steg är att lägga till min DbContext till min Startup Configuration start up class.. så vi kan injecta datacontext in i andra delar av min app..!
    public DbSet<UserReply> UserReply { get; set; } //Nästa steg är att lägga till min DbContext till min Startup Configuration start up class.. så vi kan injecta datacontext in i andra delar av min app..!
  }
}