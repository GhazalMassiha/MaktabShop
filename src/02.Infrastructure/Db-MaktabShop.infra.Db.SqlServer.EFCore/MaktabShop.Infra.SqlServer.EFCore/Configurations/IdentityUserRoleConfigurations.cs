using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabShop.Infra.SqlServer.EFCore.Configurations
{
    public class IdentityUserRoleConfigurations : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            var userRoles = new List<IdentityUserRole<int>>()
            {
                new IdentityUserRole<int>(){

                    RoleId = 1,
                    UserId = 1
                },
                new IdentityUserRole<int>(){

                    RoleId = 1,
                    UserId = 2
                },
                new IdentityUserRole<int>(){

                    RoleId = 1,
                    UserId = 3
                },
                new IdentityUserRole<int>(){

                    RoleId = 2,
                    UserId = 4
                }
            };
            builder.HasData(userRoles);
        }
    }
}
