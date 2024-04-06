namespace LeaveManagement.Identity.Configurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "611e241b-d948-499b-a43b-73d1937ea618",
                UserId = "3717acde-12a8-4dc5-af1b-d41597d2c081"
            },
            new IdentityUserRole<string>
            {
                RoleId = "939393b2-6ed5-4c56-9fb3-419eb12389d2",
                UserId = "10739c72-297a-466a-a47e-9c875244bc00"
            });
    }
}
