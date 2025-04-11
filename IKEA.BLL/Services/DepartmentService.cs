using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DataTransferObjects;
using IKEA.BLL.Factories;
using IKEA.DAL.Repositories;

namespace IKEA.BLL.Services
{
    public class DepartmentService(IDepartmentRepo _departmentRepo) : IDepartmentService
    {
        // Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepo.GetAll();

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
            var department = _departmentRepo.GetById(id);

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
            return _departmentRepo.Add(department);
        }

        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepo.Update(departmentDto.ToEntity());
        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepo.GetById(id);
            if (Department is null) return false;
            else
            {
                int Result = _departmentRepo.Remove(Department);
                return Result > 0 ? true : false;
            }
        }
    }
}
