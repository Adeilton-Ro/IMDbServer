﻿using IMDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Infra.DataBase.Mapping;
public class DirectorMap : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired();
        builder.HasMany(d => d.Films).WithOne(f => f.Director).HasForeignKey(f => f.DirectorId);
    }
}