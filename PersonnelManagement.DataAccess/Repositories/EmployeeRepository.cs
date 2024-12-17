using ErrorOr;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.DataAccess.Context;
using PersonnelManagement.DataAccess.Interfaces.Repositories;
using PersonnelManagement.DataAccess.Models;

namespace PersonnelManagement.DataAccess.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly PersonnelDbContext _personnelDbContext;
    private readonly DepartmentRepository _departmentRepository;

    public EmployeeRepository(PersonnelDbContext context, DepartmentRepository departmentRepository)
    {
        _personnelDbContext = context;
        _departmentRepository = departmentRepository;
    }

    public async Task<ErrorOr<List<EmployeeEntity>>> GetEmployeesAsync()
    {
        try
        {
            return await _personnelDbContext.Employees.AsNoTracking().ToListAsync();
        }
        catch (ArgumentNullException e)
        {
            return Error.Unexpected("Could not get employees.", e.Message);
        }
    }

    public async Task<ErrorOr<EmployeeEntity>> GetEmployeeByIdAsync(Guid id)
    {
        try
        {
            return await _personnelDbContext.Employees.AsNoTracking().FirstAsync(e => e.Id == id);
        }
        catch (InvalidOperationException e)
        {
            return Error.NotFound($"Three is no employee with id {id}.", e.Message);
        }
        catch (ArgumentNullException e)
        {
            return Error.Unexpected("Could not get employees.", e.Message);
        }
    }

    public async Task<ErrorOr<EmployeeEntity>> CreateEmployeeAsync(EmployeeEntity employee, Guid id)
    {
        try
        {
            var departmentExists = await _departmentRepository.DepartmentExistsAsync(employee.DepartmentId);
            if (departmentExists is { Value: false })
                return Error.NotFound($"Department with id {employee.DepartmentId} does not exist");

            employee.Id = id;
            var entity = await _personnelDbContext.Employees.AddAsync(employee);
            await _personnelDbContext.SaveChangesAsync();
            return entity.Entity;
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not create employee. See details", e.Message);
        }
    }

    public async Task<ErrorOr<EmployeeEntity>> CreateEmployeeAsync(EmployeeEntity employee)
    {
        try
        {
            var departmentExists = await _departmentRepository.DepartmentExistsAsync(employee.DepartmentId);
            if (departmentExists is { Value: false })
                return Error.NotFound($"Department with id {employee.DepartmentId} does not exist");

            employee.Id = Guid.NewGuid();
            var entity = await _personnelDbContext.Employees.AddAsync(employee);
            await _personnelDbContext.SaveChangesAsync();
            return entity.Entity;
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not create employee. See details", e.Message);
        }
    }

    public async Task<ErrorOr<EmployeeEntity>> UpdateEmployeeAsync(EmployeeEntity employee)
    {
        try
        {
            var departmentExists = await _departmentRepository.DepartmentExistsAsync(employee.DepartmentId);
            if (departmentExists is { IsError: false, Value: true })
                return Error.NotFound($"Department with id {employee.DepartmentId} does not exist");

            var entity = _personnelDbContext.Employees.Update(employee);
            await _personnelDbContext.SaveChangesAsync();
            return entity.Entity;
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not update employee. See details", e.Message);
        }
    }

    public async Task<ErrorOr<EmployeeEntity>> DeleteEmployeeAsync(Guid id)
    {
        try
        {
            var employee = await _personnelDbContext.Employees.FirstAsync(e => e.Id == id);
            var entity = _personnelDbContext.Employees.Remove(employee);
            await _personnelDbContext.SaveChangesAsync();
            return entity.Entity;
        }
        catch (InvalidOperationException e)
        {
            return Error.NotFound($"Nothing changed.Three is no employee with id {id}.", e.Message);
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not remove employee. See details", e.Message);
        }
    }

    public async Task<ErrorOr<bool>> EmployeeExistsAsync(Guid id)
    {
        try
        {
            return await _personnelDbContext.Employees.AnyAsync(e => e.Id == id);
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not get employee. See details", e.Message);
        }
    }

    public async Task<ErrorOr<DepartmentEntity>> GetEmployeeDepartmentAsync(Guid id)
    {
        try
        {
            var employee = await _personnelDbContext.Employees
                .Include(employeeEntity => employeeEntity.Department)
                .FirstAsync(e => e.Id == id);
            return employee.Department!;
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not get employee department. See details", e.Message);
        }
    }
}