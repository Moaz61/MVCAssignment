using IKEA.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService) : Controller
    {
        //BaseURL/Departments/Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
    }
}
