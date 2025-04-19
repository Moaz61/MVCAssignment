using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Data.Contexts;
using IKEA.DAL.Models.DepartmentModel;
using IKEA.DAL.Repositories.Interfaces;

namespace IKEA.DAL.Repositories.Classes
{
    public class DepartmentRepo(ApplicationDBContext dbContext) : GenericRepo<Department>(dbContext) , IDepartmentRepo
    {

    }
}
