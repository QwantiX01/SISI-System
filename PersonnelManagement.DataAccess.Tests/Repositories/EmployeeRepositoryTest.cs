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
    public async Task CreateAndGetEmployee_Success()
    {
        var departmentId = Guid.NewGuid();
        var testEmployee = new EmployeeEntity
        {
            DepartmentId = departmentId,
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
        };

        await _departmentRepository.CreateDepartmentAsync(
            new DepartmentEntity(name: "Test", description: "Test", addresses: [], phones: [], emails: [],
                employees: []), departmentId);

        var createResult = await _employeeRepository.CreateEmployeeAsync(testEmployee, _id);
        var getResult = await _employeeRepository.GetEmployeeByIdAsync(_id);
        
        Assert.That(getResult.IsError && createResult.IsError && !testEmployee.Equals(getResult.Value), Is.False,
            string.Join(",", getResult.Errors, createResult.Errors));
    }

    [Test]
    public async Task GetEmployeeById_Failure()
    {
        var result = await _employeeRepository.GetEmployeeByIdAsync(Guid.Empty);
        Assert.That(result.IsError, Is.True, string.Join(",", result.Errors));
    }
}