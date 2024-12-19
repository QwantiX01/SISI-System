namespace PersonnelManagement.DataAccess.Models;

public class DepartmentEntity
{
    public DepartmentEntity()
    {
    }

    public DepartmentEntity(string name, string description, List<string> addresses, List<string> phones,
        List<string> emails, List<EmployeeEntity> employees)
    {
        Name = name;
        Description = description;
        Addresses = addresses;
        Phones = phones;
        Emails = emails;
        Employees = employees;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<EmployeeEntity> Employees { get; set; } = [];
    public List<string> Addresses { get; set; } = [];
    public List<string> Phones { get; set; } = [];
    public List<string> Emails { get; set; } = [];
}