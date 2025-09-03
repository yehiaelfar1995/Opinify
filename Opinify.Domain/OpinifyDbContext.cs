using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Opinify.Domain.Entities;





namespace Opnifiy.Infrastructure
{
    public class OpinifyDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public OpinifyDbContext(DbContextOptions<OpinifyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.AnswerId, v.UserId })
                .IsUnique();

            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.AnswerId, v.IpAddress })
                .IsUnique();
        }

    }
}
