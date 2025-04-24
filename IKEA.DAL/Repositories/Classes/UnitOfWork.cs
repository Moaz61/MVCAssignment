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
        private readonly Lazy<IDepartmentRepo> _departmentRepo;
        private readonly Lazy<IEmployeeRepo> _employeeRepo;
        private readonly ApplicationDBContext _dBContext;

        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
            _departmentRepo = new Lazy<IDepartmentRepo>(() => new DepartmentRepo(dBContext));
            _employeeRepo = new Lazy<IEmployeeRepo>(() => new EmployeeRepo(dBContext));
        }
        public IEmployeeRepo EmployeeRepo => _employeeRepo.Value;

        public IDepartmentRepo DepartmentRepo => _departmentRepo.Value;

        public int SaveChanges() => _dBContext.SaveChanges();
    }
}
