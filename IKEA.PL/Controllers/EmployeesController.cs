using IKEA.BLL.DataTransferObjects.DepartmentDtos;
using IKEA.BLL.DataTransferObjects.EmployeeDtos;
using IKEA.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService , IWebHostEnvironment environment , ILogger<EmployeesController> logger) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }

        #region Create Employee
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _employeeService.CreateEmployee(employeeDto);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Can't Create Employee");
                }
                catch (Exception ex)
                {
                    if (environment.IsDevelopment()) 
                        ModelState.AddModelError(string.Empty , ex.Message);
                    else
                        logger.LogError(ex.Message);
                }
            }
            return View(employeeDto);
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


    }
}
