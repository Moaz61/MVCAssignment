using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IKEA.BLL.DataTransferObjects.EmployeeDtos;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Models.DepartmentModel;
using IKEA.DAL.Models.EmployeeModel;
using IKEA.DAL.Repositories.Interfaces;

namespace IKEA.BLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepo _employeeRepo , IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking = false)
        {
            var Employees = _employeeRepo.GetAll(WithTracking);
            //Src = Employee
            //Dest = EmployeeDto
            var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(Employees);
            return employeesDto;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepo.GetById(id);
            return employee is null ? null : _mapper.Map<Employee , EmployeeDetailsDto>(employee);
          
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            return _employeeRepo.Add(employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            return _employeeRepo.Update(_mapper.Map<UpdatedEmployeeDto , Employee>(employeeDto));
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepo.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepo.Update(employee) > 0 ? true : false;
            }
        }
    }
}
