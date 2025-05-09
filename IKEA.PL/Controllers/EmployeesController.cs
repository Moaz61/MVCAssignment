using IKEA.BLL.DataTransferObjects.DepartmentDtos;
using IKEA.BLL.DataTransferObjects.EmployeeDtos;
using IKEA.BLL.Services.Classes;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Models.EmployeeModel;
using IKEA.DAL.Models.Shared.Enums;
using IKEA.PL.ViewModels;
using IKEA.PL.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService ,
        IWebHostEnvironment environment ,
        ILogger<EmployeesController> logger) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }

        #region Create Employee
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Salary = employeeViewModel.Salary,
                        IsActive = employeeViewModel.IsActive,
                        Email = employeeViewModel.Email,
                        Gender = employeeViewModel.Gender,
                        EmployeeType = employeeViewModel.EmployeeType,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        HiringDate = employeeViewModel.HiringDate,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image,
                    };
                    int Result = _employeeService.CreateEmployee(employeeDto);
                    string Message;
                    if (Result > 0)
                        Message = $"Employee {employeeDto.Name} Is Created Successfully";
                    else
                        Message = $"Employee {employeeDto.Name} Is Not Created";

                    TempData["Message"] = Message;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (environment.IsDevelopment()) 
                        ModelState.AddModelError(string.Empty , ex.Message);
                    else
                        logger.LogError(ex.Message);
                }
            }
            return View(employeeViewModel);
        }
        #endregion

        #region Details Of Employee
        [HttpGet]
        public IActionResult Details (int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            return employee is null ? NotFound() : View(employee);
        }
        #endregion

        #region Edit Employee
        [HttpGet]
        public IActionResult Edit (int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();

            var employeeViewModel = new EmployeeViewModel()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId,
                ImageUrl=employee.Image
            };
            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit ([FromRoute]int? id , EmployeeViewModel employeeViewModel)
        {
            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return View(employeeViewModel);

            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeViewModel.Name,
                    Age = employeeViewModel.Age,
                    Address = employeeViewModel.Address,
                    Email = employeeViewModel.Email,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Gender = employeeViewModel.Gender,
                    HiringDate= employeeViewModel.HiringDate,
                    IsActive= employeeViewModel.IsActive,
                    Salary = employeeViewModel.Salary,
                    PhoneNumber= employeeViewModel.PhoneNumber,
                    DepartmentId = employeeViewModel.DepartmentId,
                    Image = employeeViewModel.Image,
                };

                var result = _employeeService.UpdateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Isn't Updated");
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty , ex.Message);
                    return View(employeeViewModel);
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion

        #region Delete Emplyee
        [HttpPost]
        public IActionResult Delete (int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion
    }
}
