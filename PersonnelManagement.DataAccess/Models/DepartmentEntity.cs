namespace PersonnelManagement.DataAccess.Models;

public class DepartmentEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<EmployeeEntity> Employees { get; set; } = [];
    public List<string> Addresses { get; set; } = [];
    public List<string> Phones { get; set; } = [];
    public List<string> Emails { get; set; } = [];
}