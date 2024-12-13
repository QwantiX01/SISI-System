namespace PersonnelManagement.DataAccess.Models;

public class EmployeeEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public DepartmentEntity? Department { get; set; }  
    public string JobTitle { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = [];
    public DateTime BirthDate { get; set; } = DateTime.MinValue;
    public DateTime EmploymentDate { get; set; } = DateTime.MinValue;
    public DateTime DismissalDate { get; set; } = DateTime.MinValue;        
    //TODO: Add Documents(noSQL), JobHistory (nullable), DepartmentEntity(new model)
}