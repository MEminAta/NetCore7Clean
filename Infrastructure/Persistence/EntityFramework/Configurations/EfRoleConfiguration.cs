using Domain.Derived;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityFramework.Configurations;

public class EfRoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasOne(x => x.CreateByUser)
            .WithMany();

        builder.HasOne(x => x.UpdateByUser)
            .WithMany();
    }
}