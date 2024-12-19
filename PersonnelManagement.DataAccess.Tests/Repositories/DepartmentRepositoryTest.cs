using JetBrains.Annotations;
using PersonnelManagement.DataAccess.Context;
using PersonnelManagement.DataAccess.Models;
using PersonnelManagement.DataAccess.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace PersonnelManagement.DataAccess.Tests.Repositories;

[TestSubject(typeof(DepartmentRepository))]
public class DepartmentRepositoryTest
{
    private static PersonnelDbContext personnelContext { get; } = new(true);
    private DepartmentRepository departmentRepo { get; set; } = new(personnelContext);

    [Fact]
    public async Task CreateDepartmentAsync_Success()
    {
        //Arrange
        var testDepartment = new DepartmentEntity(
            "TestName",
            "TestDescription",
            ["TestAddress", "TestAddress"],
            ["TestNumber", "TestNumber"],
            ["TestEmail", "TestEmail"], []);

        //Act
        var createResult = await departmentRepo.CreateDepartmentAsync(testDepartment, Guid.NewGuid());
        var getResult = await departmentRepo.GetDepartmentByIdAsync(createResult.Value.Id);

        //Assert
        Assert.False(createResult.IsError);
        Assert.False(getResult.IsError);
        Assert.Equivalent(testDepartment, getResult.Value);
    }

    [Fact]
    public async Task CreateDepartmentAsync_NullData_Failure()
    {
        //Arrange
        var testDepartment = new DepartmentEntity(
            null,
            null,
            null,
            null,
            null,
            null);

        //Act
        var result = await departmentRepo.CreateDepartmentAsync(testDepartment, Guid.NewGuid());

        //Assert
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task CreateDepartmentAsync_InvalidData_Failure()
    {
        //Arrange
        var testDepartment = new DepartmentEntity(
            "TestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestName",
            "TestDescription",
            ["TestAddress", "TestAddress"],
            ["TestNumber", "TestNumber"],
            ["TestEmail", "TestEmail"], []);

        //Act
        var createResult = await departmentRepo.CreateDepartmentAsync(testDepartment, Guid.NewGuid());
        var getResult = await departmentRepo.GetDepartmentByIdAsync(createResult.Value.Id);

        //Assert
        Assert.True(createResult.IsError);
        Assert.True(getResult.IsError);
    }
}