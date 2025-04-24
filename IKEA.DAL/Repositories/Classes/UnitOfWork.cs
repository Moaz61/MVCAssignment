using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Data.Contexts;
using IKEA.DAL.Repositories.Interfaces;

namespace IKEA.DAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDepartmentRepo _departmentRepo;
        private IEmployeeRepo _employeeRepo;
        private readonly ApplicationDBContext _dBContext;

        public UnitOfWork(IDepartmentRepo departmentRepo,
            IEmployeeRepo employeeRepo , ApplicationDBContext dBContext)
        {
            _departmentRepo = departmentRepo;
            _employeeRepo = employeeRepo;
            _dBContext = dBContext;
        }
        public IEmployeeRepo EmployeeRepo => _employeeRepo;

        public IDepartmentRepo DepartmentRepo => _departmentRepo;

        public int SaveChanges() => _dBContext.SaveChanges();
    }
}
