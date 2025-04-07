using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Repositories;

namespace IKEA.BLL.Services
{
    class DepartmentService
    {
        private readonly IDepartmentRepo _departmentRepo;

        public DepartmentService(IDepartmentRepo departmentRepo) //1.Injection
        {
            this._departmentRepo = departmentRepo;
        }
    }
}
