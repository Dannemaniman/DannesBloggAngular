using API.Controllers.Identity;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  //A DbContext instance represents a session with the database and can be used to query and save instances of your entities. 
  //DbContext is a combination of the Unit Of Work and Repository patterns.
  //Vad vi också måste göra pga vi vill accessa user roles.. och vi har gett våra entities int istället för strings.. så måste vi ge Typewriters för detta..:
  public class DataContext : IdentityDbContext<AppUser, AppRole, int, 
  IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
  IdentityRoleClaim<int>, IdentityUserToken<int>>
  {
    //Vi kommer skicka lite options till denna classen när vi lägger till den till Startup Configurationen.. 
    //där vi lägger till den till vår Dependency Injection Container:
    public DataContext(DbContextOptions options) : base(options)
    {
    }


    //IdentityDbContext, providar oss med tablesen vi behöver för att populatea vår databas med Identity.. så vi kan ta bort
    // public DbSet<AppUser> Users { get; set; } //Nästa steg är att lägga till min DbContext till min Startup Configuration start up class.. så vi kan injecta datacontext in i andra delar av min app..!
    public DbSet<UserThread> UserThreads { get; set; } //Nästa steg är att lägga till min DbContext till min Startup Configuration start up class.. så vi kan injecta datacontext in i andra delar av min app..!
    public DbSet<UserReply> UserReplies { get; set; } //Nästa steg är att lägga till min DbContext till min Startup Configuration start up class.. så vi kan injecta datacontext in i andra delar av min app..!
    public DbSet<UserBlockList> UserBlockList { get; set; } //Nästa steg är att lägga till min DbContext till min Startup Configuration start up class.. så vi kan injecta datacontext in i andra delar av min app..!
  
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<AppUser>()
        .HasMany(ur => ur.UserRoles)
        .WithOne(u => u.User)
        .HasForeignKey(ur => ur.UserId)
        .IsRequired();

      builder.Entity<AppRole>()
        .HasMany(ur => ur.UserRoles)
        .WithOne(u => u.Role)
        .HasForeignKey(ur => ur.RoleId)
        .IsRequired();  
    }
  }
}