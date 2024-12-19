using System.Data;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.DataAccess.Models;

namespace PersonnelManagement.DataAccess.Context;

public sealed class PersonnelDbContext : DbContext
{
    private readonly bool _test;

    public PersonnelDbContext(bool test = false)
    {
        _test = test;
        Database.EnsureCreated();
    }

    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<DepartmentEntity> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_test)
        {
            optionsBuilder.UseSqlite("Data Source= PersonnelDb.db;");
        }
        else
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=PersonnelDb;Username=postgres;Password=postgres");
        }
    }
}