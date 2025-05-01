using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IKEA.BLL.DataTransferObjects.EmployeeDtos;
using IKEA.BLL.Services.AttachmentService;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Models.DepartmentModel;
using IKEA.DAL.Models.EmployeeModel;
using IKEA.DAL.Repositories.Interfaces;

namespace IKEA.BLL.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork,
        IMapper _mapper, IAttachmentService _attachmentService) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees = _unitOfWork.EmployeeRepo.GetAll();
            else
                employees = _unitOfWork.EmployeeRepo.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));

            var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return employeesDto;

        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepo.GetById(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);

        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null)
            {
                employee.ImageName = _attachmentService.Upload(employeeDto.Image, "Images");
            }

            _unitOfWork.EmployeeRepo.Add(employee); // Add Locally

            return _unitOfWork.SaveChanges();
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null)
            {
                employee.ImageName = _attachmentService.Upload(employeeDto.Image, "Images");
            }
            _unitOfWork.EmployeeRepo.Update(employee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepo.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepo.Update(employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }
    }
}
