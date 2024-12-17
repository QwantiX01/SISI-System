using ErrorOr;
using PersonnelManagement.DataAccess.Models;

namespace PersonnelManagement.DataAccess.Interfaces.Repositories;

public interface IEmployeeRepository
{
    public Task<ErrorOr<List<EmployeeEntity>>> GetEmployeesAsync();
    public Task<ErrorOr<EmployeeEntity>> GetEmployeeByIdAsync(Guid id);
    public Task<ErrorOr<EmployeeEntity>> CreateEmployeeAsync(EmployeeEntity employee);
    public Task<ErrorOr<EmployeeEntity>> UpdateEmployeeAsync(EmployeeEntity employee);
    public Task<ErrorOr<EmployeeEntity>> DeleteEmployeeAsync(Guid id);
    public Task<ErrorOr<bool>> EmployeeExistsAsync(Guid id);
    public Task<ErrorOr<DepartmentEntity>> GetEmployeeDepartmentAsync(Guid id);
}