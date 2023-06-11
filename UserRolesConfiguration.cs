using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jwt.Configuration
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                 new IdentityUserRole<string>()
                 {
                     RoleId = "b31f9c27-4c2d-4f82-9aa8-a5a10382ca98",
                     UserId = "634a60ec-96bc-4f37-937f-27ba52f58d41"
                 },
                  new IdentityUserRole<string>() 
                 {
                      RoleId = "fa6f6c49-37f8-4447-a6bd-1c0a2a1353dc", 
                      UserId = "c80d49e8-a97c-45e6-babf-0760a6b86814" 
                  }

                );
        }
    }
}
