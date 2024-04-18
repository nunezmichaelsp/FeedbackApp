namespace FeedbackApp.Data
{
    using FeedbackApp.Domain.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Feedback>().HasKey(f => f.Id);
            builder.Entity<Feedback>().Property(f => f.CustomerName).IsRequired();
            builder.Entity<Feedback>().Property(f => f.Category).IsRequired();
            builder.Entity<Feedback>().Property(f => f.Description).IsRequired();
        }
    }
}
