using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelManagement.DataAccess.Models;

namespace PersonnelManagement.DataAccess.Configurations;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    private const int FirstNameMaxLength = 40;
    private const int MiddleNameMaxLength = 40;
    private const int LastNameMaxLength = 60;
    private const int AddressMaxLength = 200;
    private const int EmailMaxLength = 100;
    private const int PhoneNumberMaxLength = 20;
    private const int JobTitleMaxLength = 70;
    private const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    private const string PhoneRegex = @"^\+?[1-9]\d{1,14}$";

    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.FirstName)
            .HasMaxLength(FirstNameMaxLength)
            .IsRequired();

        builder.Property(e => e.MiddleName)
            .HasMaxLength(MiddleNameMaxLength)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasMaxLength(LastNameMaxLength)
            .IsRequired();

        builder.Property(e => e.Address)
            .HasMaxLength(AddressMaxLength)
            .IsRequired(false);

        builder.HasIndex(e => e.Email)
            .IsUnique();
        builder.Property(e => e.Email)
            .HasMaxLength(EmailMaxLength)
            .HasAnnotation("RegularExpression", EmailRegex)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(PhoneNumberMaxLength)
            .HasAnnotation("RegularExpression", PhoneRegex)
            .IsRequired();

        builder.Property(e => e.BirthDate)
            .IsRequired();

        builder.Property(e => e.JobTitle)
            .HasMaxLength(JobTitleMaxLength)
            .IsRequired();

        builder
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees);

        builder.Property(e => e.DepartmentId)
            .IsRequired();

        builder.Property(e => e.Skills)
            .IsRequired();
    }
}