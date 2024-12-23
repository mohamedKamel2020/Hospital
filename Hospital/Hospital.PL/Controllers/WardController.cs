using Hospital.BLL.Interfaces;
using Hospital.DAL.Context;
using Hospital.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.PL.Controllers
{
    public class WardController : Controller
    {
        private readonly IGenericRepository<Ward> _wardRepository;
        private readonly HospitalDbContext _dbContext;


        public WardController(IGenericRepository<Ward> wardRepository, HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
            _wardRepository = wardRepository;
        }

        public IActionResult Index()
        {
            var wards = _wardRepository.GetAll();
            return View(wards);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_dbContext.Depatments, "Departmentid", "Departmentname");
            return View();

        }
        [HttpPost]
        public IActionResult Create(Ward ward)
        {

            _wardRepository.Add(ward);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            if (id == null)
                return NotFound();
            var ward = _wardRepository.Get(id);

            if (ward == null)
                return NotFound();
            return View(ward);
        }
        public IActionResult Edit(int id)
        {
            var ward = _wardRepository.Get(id);
            ViewBag.Departments = new SelectList(_dbContext.Depatments, "Departmentid", "Departmentname");
            return View(ward);
        }

        [HttpPost]
        public IActionResult Edit(int id, Ward ward)
        {
            try
            {
                _wardRepository.Update(ward);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message); // 3.NotFrindly Message
                return View(ward);
            }
        }

        public IActionResult Delete(int id)
        {
            var ward = _wardRepository.Get(id);
            ViewBag.Departments = new SelectList(_dbContext.Depatments, "Departmentid", "Departmentname");
            return View(ward);
        }
        [HttpPost]
        public IActionResult Delete(int id, Ward ward)
        {
            try
            {

                _wardRepository.Delete(ward.Wardid);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(ward);
            }
        }

    }
}
