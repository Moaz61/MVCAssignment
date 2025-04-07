using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Data.Contexts;

namespace IKEA.DAL.Repositories
{
    //Primary Constructor .Net8 C#12
    class DepartmentRepo(ApplicationDBContext dbContext)
    {
        private readonly ApplicationDBContext _dbContext = dbContext;

        //CRUD Operations
        //Get All
        //Get By Id
        public Department? GetById (int id)
        {
           var department = _dbContext.Departments.Find(id);
            return department;
        }

        //Insert
        //Update
        //Delete
    }
}
