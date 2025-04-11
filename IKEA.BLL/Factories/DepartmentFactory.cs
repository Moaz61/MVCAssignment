using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DataTransferObjects;
using IKEA.DAL.Models;

namespace IKEA.BLL.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            return new DepartmentDto()
            {
                DepId = D.Id,
                Code = D.Code,
                Description = D.Description,
                Name = D.Name,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Code=department.Code,
                Description=department.Description,
                CreatedBy=department.CreatedBy,
                Name = department.Name,
                LastModifiedBy =  department.LastModifiedBy,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn)
            };
        }

        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

        public static Department ToEntity(this UpdatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code= departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
    }
}
