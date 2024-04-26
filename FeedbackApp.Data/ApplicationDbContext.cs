namespace FeedbackApp.Data
{
    using FeedbackApp.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
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

            var hasher = new PasswordHasher<IdentityUser>();

            var superAdminUser = new IdentityUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                UserName = "superadmin@domain.com",
                NormalizedUserName = "SUPERADMIN@DOMAIN.COM",
                Email = "superadmin@domain.com",
                NormalizedEmail = "SUPERADMIN@DOMAIN.COM",
                SecurityStamp = "2361e5d4910a42dc9848a6c2b502f592",
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
            };

            superAdminUser.PasswordHash = hasher.HashPassword(superAdminUser, "Abc123.");

            builder.Entity<IdentityUser>().HasData(superAdminUser);
        }
    }
}
