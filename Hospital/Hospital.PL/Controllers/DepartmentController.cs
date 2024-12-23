using Hospital.BLL.Interfaces;
using Hospital.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IGenericRepository<Depatment> _departmentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DepartmentController(IGenericRepository<Depatment> departmentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _departmentRepository = departmentRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Depatment department, IFormFile fileicon)
        {
            if (fileicon != null)
            {
                var fileName = Path.GetFileName(fileicon.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/departments", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                { fileicon.CopyToAsync(fileStream); }
                department.fileicon = fileName;
            }
            _departmentRepository.Add(department);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            if (id == null)
                return NotFound();
            var department = _departmentRepository.Get(id);
            if (department == null)
                return NotFound();
            return View(department);
        }
        public IActionResult Edit(int id)
        {
            return View(id);
        }
        [HttpPost]
        public IActionResult Edit(int id, Depatment depatment)
        {
            try
            {
                _departmentRepository.Update(depatment);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(depatment);
            }
        }
    }
}
