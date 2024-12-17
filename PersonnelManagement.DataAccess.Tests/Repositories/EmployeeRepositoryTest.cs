using Microsoft.EntityFrameworkCore;
using PersonnelManagement.DataAccess.Context;
using PersonnelManagement.DataAccess.Models;
using PersonnelManagement.DataAccess.Repositories;

namespace PersonnelManagement.DataAccess.Tests.Repositories;

[TestFixture]
[TestOf(typeof(EmployeeRepository))]
public class EmployeeRepositoryTest
{
    private PersonnelDbContext _personnelDbContext { get; } = new(true);
    private EmployeeRepository _employeeRepository { get; set; }
    private DepartmentRepository _departmentRepository { get; set; }
    private Guid _id { get; set; }

    [SetUp]
    public void Setup()
    {
        _id = Guid.NewGuid();
        _departmentRepository = new DepartmentRepository(_personnelDbContext);
        _employeeRepository = new EmployeeRepository(_personnelDbContext, _departmentRepository);
    }

    [Test]
    public async Task GetAllEmployees_Success()
    {
        var result = await _employeeRepository.GetEmployeesAsync();
        Assert.That(result.IsError, Is.False, string.Join(",", result.Errors));
    }

    [Test]
    public async Task CreateEmployee_Success()
    {
        var result = await _employeeRepository.CreateEmployeeAsync(new EmployeeEntity
        {
            DepartmentId = Guid.NewGuid(),
            FirstName = "Test",
            MiddleName = "Test",
            LastName = "Test",
            Address = "Test",
            Email = "Test",
            PhoneNumber = "Test",
            JobTitle = "Test",
            Skills = ["Test", "Test"],
            BirthDate = DateTime.Now,
            EmploymentDate = DateTime.Now,
            DismissalDate = DateTime.Now
        }, _id);
        
        Assert.That(result.IsError, Is.False, string.Join(",", result.Errors));
    }

    [Test]
    public async Task GetEmployeeById_Success()
    {
        var result = await _employeeRepository.GetEmployeeByIdAsync(_id);
        Assert.That(result.IsError, Is.False, string.Join(",", result.Errors));
    }
}