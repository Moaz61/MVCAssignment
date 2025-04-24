using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DataTransferObjects.DepartmentDtos;
using IKEA.BLL.Factories;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Repositories.Interfaces;

namespace IKEA.BLL.Services.Classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        // Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepo.GetAll();

            #region Mapping Method
            ///Mapping
            //var DepartmentsToReturn = departments.Select(D => new DepartmentDto
            //{
            //    DepId = D.Id,
            //    Code = D.Code,
            //    Description = D.Description,
            //    Name = D.Name,
            //    DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            //});
            //return DepartmentsToReturn; 
            #endregion

            //Extension Method From DepartmentFactory
            return departments.Select(D => D.ToDepartmentDto());
        }

        //Get Department By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepo.GetById(id);

            #region Manual Mapping
            //return department is null ? null : new DepartmentDetailsDto
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn)
            //}; 
            #endregion

            //Extension Method
            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        //Create New Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepo.Add(department);
            return _unitOfWork.SaveChanges();
        }

        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepo.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var Department = _unitOfWork.DepartmentRepo.GetById(id);
            if (Department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepo.Remove(Department);
                int Result = _unitOfWork.SaveChanges();
                return Result > 0 ? true : false;
            }
        }
    }
}
