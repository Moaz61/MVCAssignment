using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Data.Contexts;
using IKEA.DAL.Models.EmployeeModel;
using IKEA.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Repositories.Classes
{
    public class EmployeeRepo(ApplicationDBContext dbContext) : GenericRepo<Employee>(dbContext), IEmployeeRepo
    {
    
    }
}
