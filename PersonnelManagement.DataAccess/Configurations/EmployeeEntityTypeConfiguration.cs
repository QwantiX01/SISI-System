using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelManagement.DataAccess.Models;

namespace PersonnelManagement.DataAccess.Configurations;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.Property(e => e.FirstName).HasMaxLength(40);
        builder.Property(e => e.LastName).HasMaxLength(60);
        // TODO: (add regex validation) builder.HasIndex(e => e.Email).; ()
    }
}