using Hospital.DAL.Context;
using Hospital.DAL.Models;
using Hospital.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.PL.Controllers
{
    public class StaffController : Controller
    {
        private readonly HospitalDbContext _dbContext;

        public StaffController(HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: Hospital
        public IActionResult Index()
        {
            var viewModel = new StaffViewModel
            {
                Doctors = _dbContext.Doctors.Include(d => d.Dep).ToList(),
                Nurses = _dbContext.Nurses.Include(N => N.Ward).ThenInclude(W => W.DepatmentnoNavigation).ToList(),
                Accountants = _dbContext.Accountants.ToList()
            };
            return View(viewModel);
        }

        public IActionResult AddDoctor()
        {
            ViewBag.Departments = new SelectList(_dbContext.Depatments, "Departmentid", "Departmentname");
            return View();
        }
        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            _dbContext.Add(doctor);
            _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditDoctor(int? id)
        {
            if (id == null) { return NotFound(); }
            var doctor = _dbContext.Doctors.Find(id);
            if (doctor == null) { return NotFound(); }
            return View(doctor);
        }
        [HttpPost]
        public IActionResult EditDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Doctorid)
            { return NotFound(); }
            else
            {
                _dbContext.Update(doctor);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteDoctor(int? id)
        {

            var doctor = _dbContext.Doctors.Include(d => d.Dep).FirstOrDefault(m => m.Doctorid == id);
            if (doctor == null) { return NotFound(); }
            return View(doctor);
        }

        [HttpPost, ActionName("DeleteDoctor")]
        public IActionResult DeleteDoctorConfirmed(int id)
        {
            var doctor = _dbContext.Doctors.Find(id);
            _dbContext.Doctors.Remove(doctor);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Nurse

        public IActionResult AddNurse()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNurse(Nurse nurse)
        {
            _dbContext.Nurses.Add(nurse);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditNurse(int? id)
        {
            if (id == null) { return NotFound(); }
            var nurse = _dbContext.Nurses.Find(id);
            if (nurse == null) { return NotFound(); }
            return View(nurse);
        }
        [HttpPost]
        public IActionResult EditNurse(int id, Nurse nurse)
        {
            if (id != nurse.Nurseid)
            { return NotFound(); }
            else
            {
                _dbContext.Nurses.Update(nurse);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteNurse(int? id)
        {

            var nurse = _dbContext.Nurses.Include(N => N.Ward).FirstOrDefault(n => n.Nurseid == id);
            if (nurse == null) { return NotFound(); }
            return View(nurse);
        }

        [HttpPost, ActionName("DeleteNurse")]
        public IActionResult DeleteNurseConfirmed(int id)
        {
            var nurse = _dbContext.Nurses.Find(id);
            _dbContext.Nurses.Remove(nurse);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Accountant

        public IActionResult AddAccountant()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAccountant(Accountant accountant)
        {
            _dbContext.Accountants.Add(accountant);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditAccountant(int? id)
        {
            if (id == null) { return NotFound(); }
            var accountant = _dbContext.Accountants.Find(id);
            if (accountant == null) { return NotFound(); }
            return View(accountant);
        }
        [HttpPost]
        public IActionResult EditAccountant(int id, Accountant accountant)
        {
            if (id != accountant.Accountantid)
            { return NotFound(); }
            else
            {
                _dbContext.Accountants.Update(accountant);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteAccountant(int? id)
        {

            var accountant = _dbContext.Accountants.Include(A => A.Bills).FirstOrDefault(a => a.Accountantid == id);
            if (accountant == null) { return NotFound(); }
            return View(accountant);
        }

        [HttpPost, ActionName("DeleteAccountant")]
        public IActionResult DeleteAccountantConfirmed(int id)
        {
            var accountant = _dbContext.Accountants.Find(id);
            _dbContext.Accountants.Remove(accountant);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
