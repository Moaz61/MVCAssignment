using IKEA.BLL.DataTransferObjects;
using IKEA.BLL.DataTransferObjects.DepartmentDtos;
using IKEA.BLL.Services.Interfaces;
using IKEA.PL.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService ,
        ILogger<DepartmentController> _logger ,
        IWebHostEnvironment _environment): Controller
    {
        //BaseURL/Department/Index
        public IActionResult Index()
        {
            //ViewData VS ViewBag

            //ViewData["Message"] = new DepartmentDto() { Name = "TestViewData" };
            //ViewBag.Message = new DepartmentDto() { Name = "TestViewBag" };

            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        #region Create Department
        [HttpGet]
        public IActionResult Create() => View();
        
        [HttpPost]
        //[ValidateAntiForgeryToken] // Action Filter
        /*3mlnaha fy program 3nd AddControllersWithView 
          3shan tghyr kol 7aga 3ndy m7taga Validation 
          bdl m 23mlha b nfsy f kol files
        */
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if(ModelState.IsValid) //Server Side Validation
            { 
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        Description = departmentViewModel.Description,
                        DateOfCreation = departmentViewModel.DateOfCreation,
                    };
                    int Result = _departmentService.AddDepartment(departmentDto);
                    string Message;
                    if (Result > 0)
                        Message = $"Department {departmentViewModel.Name} Is Created Successfully";
                    else
                        Message = $"Department {departmentViewModel.Name} Is Not Created";

                    TempData["Message"] = Message ;
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    if(_environment.IsDevelopment())
                    {
                        // 1. Development => Log Error In Console And Return Same View With Error Message
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        // 2. Deployment => Log Error In File | Table In Database And Return Error View
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(departmentViewModel);  
        }

        #endregion

        #region Details Of Department
        [HttpGet]
        public IActionResult Details(int? id)
        { 
            if(!id.HasValue) return BadRequest(); //400
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound(); //404
            return View(department);
        }
        #endregion

        #region Edit Department
        [HttpGet]
        public IActionResult Edit (int? id)
        {
            if(!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };
            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id , DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdatedDepartment = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Code = viewModel.Code,
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.DateOfCreation
                    };
                    int Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is not Updated");
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        // 1. Development => Log Error In Console And Return Same View With Error Message
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        // 2. Deployment => Log Error In File | Table In Database And Return Error View
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                    }
                }
            }
            return View(viewModel);
        }
        #endregion

        #region Delete Department
        //[HttpGet]
        //public IActionResult Delete (int? id)
        //{
        //    if(!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete (int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Is Not Deleted");
                    return RedirectToAction(nameof(Delete) , new { id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Development => Log Error In Console And Return Same View With Error Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // 2. Deployment => Log Error In File | Table In Database And Return Error View
                    _logger.LogError(ex.Message);
                    return View("ErrorView" , ex);
                }
            }
        }
        #endregion
    }
}
