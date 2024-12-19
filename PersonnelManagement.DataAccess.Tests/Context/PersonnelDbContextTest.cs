using PersonnelManagement.DataAccess.Context;

namespace PersonnelManagement.DataAccess.Tests.Context;

[TestFixture]
[TestOf(typeof(PersonnelDbContext))]
public class PersonnelDbContextTest
{
    [Test]
    public void Initialization_Test_Db()
    {
        var context = new PersonnelDbContext(true);
        context.Database.EnsureCreated();
    }

    [Test]
    public void Initialization_Main_Db()
    {
        var context = new PersonnelDbContext();
        context.Database.EnsureCreated();
    }
}