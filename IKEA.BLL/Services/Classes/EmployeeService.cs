using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DataTransferObjects.EmployeeDtos;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Models.DepartmentModel;
using IKEA.DAL.Repositories.Interfaces;

namespace IKEA.BLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepo _employeeRepo) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking = false)
        {
            var Employees = _employeeRepo.GetAll(WithTracking);
            var employeesDto = Employees.Select(Emp => new EmployeeDto()
            {
                Id = Emp.Id,
                Name = Emp.Name,
                Age = Emp.Age,
                Email = Emp.Email,
                IsActive = Emp.IsActive,
                Salary = Emp.Salary,
                EmployeeType = Emp.EmployeeType.ToString(),
                Gender = Emp.Gender.ToString(),
            });
            return employeesDto;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepo.GetById(id);
            return employee is null ? null : new EmployeeDetailsDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                Email = employee.Email,
                HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                EmployeeType = employee.EmployeeType.ToString(),
                Gender = employee.Gender.ToString(),
                CreatedBy = 1,
                CreatedOn = employee.CreatedOn,
                LastModifiedBy = 1,
                LastModifiedOn = employee.LastModifiedOn.Value,
            };
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

     
    }
}
