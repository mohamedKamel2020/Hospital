using Hospital.BLL.Interfaces;
using Hospital.DAL.Models;
using Hospital.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hospital.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Patient> _patientRepository;

        public HomeController(IGenericRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public IActionResult Index()
        {
            var patients = _patientRepository.GetAll();
            return View(patients);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Patient patient)
        {

            _patientRepository.Add(patient);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Details(int id)
        {
            if (id == null)
                return NotFound();
            var patient = _patientRepository.Get(id);

            if (patient == null)
                return NotFound();
            return View(patient);
        }

        public IActionResult Edit(int id)
        {
            return Details(id);
        }

        [HttpPost]
        public IActionResult Edit(int id, Patient patient)
        {
            try
            {
                _patientRepository.Update(patient);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message); // 3.NotFrindly Message
                return View(patient);
            }
        }

        public IActionResult Delete(int id)
        {
            return Details(id);
        }
        [HttpPost]
        public IActionResult Delete(int id, Patient patient)
        {

            try
            {
                _patientRepository.Delete(patient.Patientid);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(patient);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
