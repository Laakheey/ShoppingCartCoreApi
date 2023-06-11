using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jwt.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasData(
                new IdentityUser
                {
                    Id = "634a60ec-96bc-4f37-937f-27ba52f58d41",
                    UserName = "admin123@gmail.com",
                    NormalizedUserName = "admin123@gmail.com",
                    Email = "admin123@gmail.com",
                    NormalizedEmail = "admin123@gmail.com",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "admin@123")
                },
                 new IdentityUser
                 {
                     Id = "c80d49e8-a97c-45e6-babf-0760a6b86814",
                     UserName = "user123@gmail.com",
                     NormalizedUserName = "user123@gmail.com",
                     Email = "user123@gmail.com",
                     NormalizedEmail = "user123@gmail.com",
                     PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "user@123")
                 }
                );
        }
    }
}
