using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jwt.Configuration
{
    public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {

                     Id = "b31f9c27-4c2d-4f82-9aa8-a5a10382ca98", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin"
                },
                new IdentityRole
                {
                     Id = "fa6f6c49-37f8-4447-a6bd-1c0a2a1353dc", Name = "User", ConcurrencyStamp = "2", NormalizedName = "User"
                }
                );   
        }
    }
}
