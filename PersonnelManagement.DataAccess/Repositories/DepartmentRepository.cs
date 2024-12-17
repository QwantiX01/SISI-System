using ErrorOr;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.DataAccess.Context;
using PersonnelManagement.DataAccess.Interfaces.Repositories;
using PersonnelManagement.DataAccess.Models;

namespace PersonnelManagement.DataAccess.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly PersonnelDbContext _personnelDbContext;

    public DepartmentRepository(PersonnelDbContext context)
    {
        _personnelDbContext = context;
    }


    public async Task<ErrorOr<List<DepartmentEntity>>> GetDepartmentsAsync()
    {
        try
        {
            return await _personnelDbContext.Departments.AsNoTracking().ToListAsync();
        }
        catch (ArgumentNullException e)
        {
            return Error.Unexpected("Could not get departments.", e.Message);
        }
    }

    public async Task<ErrorOr<DepartmentEntity>> GetDepartmentByIdAsync(Guid id)
    {
        try
        {
            return await _personnelDbContext.Departments.AsNoTracking().FirstAsync(d => d.Id == id);
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

    public async Task<ErrorOr<DepartmentEntity>> CreateDepartmentAsync(DepartmentEntity department)
    {
        try
        {
            department.Id = Guid.NewGuid();
            var entity = await _personnelDbContext.Departments.AddAsync(department);
            await _personnelDbContext.SaveChangesAsync();
            return entity.Entity;
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not create department. See details", e.Message);
        }
    }

    public async Task<ErrorOr<DepartmentEntity>> CreateDepartmentAsync(DepartmentEntity department, Guid id)
    {
        try
        {
            department.Id = id;
            var entity = await _personnelDbContext.Departments.AddAsync(department);
            await _personnelDbContext.SaveChangesAsync();
            return entity.Entity;
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not create department. See details", e.Message);
        }
    }

    public async Task<ErrorOr<DepartmentEntity>> UpdateDepartmentAsync(DepartmentEntity department)
    {
        try
        {
            var entity = _personnelDbContext.Departments.Update(department);
            await _personnelDbContext.SaveChangesAsync();
            return entity.Entity;
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not update department. See details", e.Message);
        }
    }

    public async Task<ErrorOr<DepartmentEntity>> DeleteDepartmentAsync(Guid id)
    {
        try
        {
            var department = await _personnelDbContext.Departments.FirstAsync(d => d.Id == id);
            var entity = _personnelDbContext.Departments.Remove(department);
            await _personnelDbContext.SaveChangesAsync();
            return entity.Entity;
        }
        catch (InvalidOperationException e)
        {
            return Error.NotFound($"Nothing changed. Three is no department with id {id}.", e.Message);
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not remove department. See details", e.Message);
        }
    }

    public async Task<ErrorOr<bool>> DepartmentExistsAsync(Guid id)
    {
        try
        {
            return await _personnelDbContext.Departments.AnyAsync(d => d.Id == id);
        }
        catch (Exception e)
        {
            return Error.Unexpected("Could not get department. See details", e.Message);
        }
    }

    public async Task<ErrorOr<List<EmployeeEntity>>> GetDepartmentEmployeesAsync(Guid id)
    {
        return await _personnelDbContext.Employees.Where(e => e.DepartmentId == id).ToListAsync();
    }
}