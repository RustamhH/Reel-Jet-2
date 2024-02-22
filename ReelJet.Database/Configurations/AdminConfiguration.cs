using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin> {

    public void Configure(EntityTypeBuilder<Admin> builder) {

        builder.Property(p => p.Name);
        builder.Property(p => p.Surname);
        builder.Property(p => p.Username).IsRequired();
        builder.Property(p => p.Avatar);
        builder.Property(p => p.Password).IsRequired();
        builder.Property(p => p.Email).IsRequired();
    }
}