using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityFramework.Configurations;

public class EfUserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(x => x.CreateByUser)
            .WithMany();

        builder.HasOne(x => x.UpdateByUser)
            .WithMany();

    }
}