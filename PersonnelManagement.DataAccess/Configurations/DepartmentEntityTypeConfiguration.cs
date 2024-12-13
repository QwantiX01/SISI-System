using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelManagement.DataAccess.Models;

namespace PersonnelManagement.DataAccess.Configurations;

public class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<DepartmentEntity>
{
    private const int NameMaxLength = 100;
    private const int DescriptionMaxLength = 500;
    
    public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(d => d.Name)
            .IsRequired() 
            .HasMaxLength(NameMaxLength); 

        builder.Property(d => d.Description)
            .HasMaxLength(DescriptionMaxLength);

        //TODO: Learn this(Conversion)
        builder.Property(d => d.Addresses)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        builder.Property(d => d.Phones)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        builder.Property(d => d.Emails)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        builder.HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId);
    }
}