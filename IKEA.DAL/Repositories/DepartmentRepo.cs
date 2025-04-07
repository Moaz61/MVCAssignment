using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Data.Contexts;

namespace IKEA.DAL.Repositories
{
    //Primary Constructor .Net8 C#12
    public class DepartmentRepo(ApplicationDBContext dbContext) : IDepartmentRepo
    {
        private readonly ApplicationDBContext _dbContext = dbContext;

        //Get All
        public IEnumerable<Department> GetAll(bool WithTracker = false)
        {
            if (WithTracker)
                return _dbContext.Departments.ToList();
            else
                return _dbContext.Departments.AsNoTracking().ToList();
        }

        //Get By Id
        public Department? GetById(int id) => _dbContext.Departments.Find(id);

        //Update
        public int Update(Department department)
        {
            _dbContext.Departments.Update(department); //Update Locally
            return _dbContext.SaveChanges();
        }

        //Delete
        public int Remove(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        //Insert
        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

    }
}
