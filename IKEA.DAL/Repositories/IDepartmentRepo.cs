
namespace IKEA.DAL.Repositories
{
    public interface IDepartmentRepo
    {
        int Add(Department department);
        IEnumerable<Department> GetAll(bool WithTracker = false);
        Department? GetById(int id);
        int Remove(Department department);
        int Update(Department department);
    }
}