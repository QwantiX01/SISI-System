using ErrorOr;
using PersonnelManagement.DataAccess.Models;

namespace PersonnelManagement.DataAccess.Interfaces.Repositories;

public interface IDepartmentRepository
{
    public Task<ErrorOr<List<DepartmentEntity>>> GetDepartmentsAsync();
    public Task<ErrorOr<DepartmentEntity>> GetDepartmentByIdAsync(Guid id);
    public Task<ErrorOr<DepartmentEntity>> CreateDepartmentAsync(DepartmentEntity department);
    public Task<ErrorOr<DepartmentEntity>> CreateDepartmentAsync(DepartmentEntity department, Guid id);
    public Task<ErrorOr<DepartmentEntity>> UpdateDepartmentAsync(DepartmentEntity department);
    public Task<ErrorOr<DepartmentEntity>> DeleteDepartmentAsync(Guid id);
    public Task<ErrorOr<bool>> DepartmentExistsAsync(Guid id);
    public Task<ErrorOr<List<EmployeeEntity>>> GetDepartmentEmployeesAsync(Guid id);
}