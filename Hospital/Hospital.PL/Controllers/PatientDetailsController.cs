using Hospital.DAL.Context;
using Hospital.DAL.Models;
using Hospital.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.PL.Controllers
{
    public class PatientDetailsController : Controller
    {
        private readonly HospitalDbContext _dbContext;

        public PatientDetailsController(HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var viewModel = new PatientDetailsViewModel
            {
                Bills = _dbContext.Bills
                .Include(B => B.Pat)
                .Include(B => B.Acc)
                .ToList(),

                MedicalRecords = _dbContext.Medicalrecords
                .Include(R => R.Pat).ToList(),

                DoctorWriteTestReports = _dbContext.Doctorwritetestreports
               .Include(D => D.Doc)
               .Include(D => D.Pat)
               .Include(D => D.Testreporttreatment)
               .ToList(),

                Testreporttreatments = _dbContext.Testreporttreatments
               .Include(T => T.Doctorwritetestreport)
               .ToList(),


            };
            return View(viewModel);
        }

        public IActionResult AddReport()
        {
            ViewBag.Doctors = new SelectList(_dbContext.Doctors, "Doctorid", "Doctorname");
            ViewBag.Patients = new SelectList(_dbContext.Patients, "Patientid", "Patientname");

            return View();
        }
        [HttpPost]
        public IActionResult AddReport(Doctorwritetestreport doctorwritetestreport)
        {
            _dbContext.Add(doctorwritetestreport);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditReport(int doctorId, int patientId)
        {
            var report = _dbContext.Doctorwritetestreports.FirstOrDefault(d => d.Docid == doctorId && d.Patid == patientId);
            if (report == null) { return NotFound(); }
            return View(report);
        }

        [HttpPost]
        public IActionResult EditReport(int doctorId, int patientId, Doctorwritetestreport doctorwritetestreport)
        {
            if (doctorId != doctorwritetestreport.Docid || patientId != doctorwritetestreport.Patid) { return NotFound(); }
            _dbContext.Update(doctorwritetestreport);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult DeleteReport(int doctorId, int patientId)
        {
            if (doctorId == null || patientId == null)
            { return NotFound(); }
            var doctorWriteTestReport = _dbContext.Doctorwritetestreports
                .FirstOrDefault(d => d.Docid == doctorId && d.Patid == patientId);
            if (doctorWriteTestReport == null) { return NotFound(); }
            return View(doctorWriteTestReport);
        }

        [HttpPost, ActionName("DeleteReport")]
        public IActionResult DeleteReportConfirmed(int doctorId, int patientId)
        {
            var doctorWriteTestReport = _dbContext.Doctorwritetestreports
                .FirstOrDefault(d => d.Docid == doctorId && d.Patid == patientId);
            _dbContext.Doctorwritetestreports.Remove(doctorWriteTestReport);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Treatment
        public IActionResult AddTreatment(int doctorId, int patientId)
        {
            ViewBag.Doctors = new SelectList(_dbContext.Doctors, "Doctorid", "Doctorname");
            ViewBag.Patients = new SelectList(_dbContext.Patients, "Patientid", "Patientname");

            ViewBag.DoctorId = doctorId;
            ViewBag.PatientId = patientId;

            return View();
        }
        [HttpPost]
        public IActionResult AddTreatment(Testreporttreatment testreporttreatment)
        {
            _dbContext.Add(testreporttreatment);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditTreatment(int doctorId, int patientId)
        {
            var testreporttreatment = _dbContext.Testreporttreatments
                .FirstOrDefault(T => T.Docid == doctorId && T.Patid == patientId);
            if (testreporttreatment == null) { return NotFound(); }
            return View(testreporttreatment);
        }
        [HttpPost]
        public IActionResult EditTreatment(int doctorId, int patientId, Testreporttreatment testreporttreatment)
        {
            if (doctorId != testreporttreatment.Docid || patientId != testreporttreatment.Patid) { return NotFound(); }
            _dbContext.Update(testreporttreatment);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult DeleteTreatment(int doctorId, int patientId)
        {
            if (doctorId == null || patientId == null)
            { return NotFound(); }
            var treatment = _dbContext.Testreporttreatments
                .FirstOrDefault(d => d.Docid == doctorId && d.Patid == patientId);
            if (treatment == null) { return NotFound(); }
            return View(treatment);
        }

        [HttpPost, ActionName("DeleteTreatment")]
        public IActionResult DeleteTreatmentConfirmed(int doctorId, int patientId)
        {
            var doctorWriteTestReport = _dbContext.Testreporttreatments
                .FirstOrDefault(d => d.Docid == doctorId && d.Patid == patientId);
            _dbContext.Testreporttreatments.Remove(doctorWriteTestReport);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Bill
        public IActionResult AddBill()
        {
            ViewBag.Accountants = new SelectList(_dbContext.Accountants, "Accountantid", "Accountantname");
            ViewBag.Patients = new SelectList(_dbContext.Patients, "Patientid", "Patientname");

            return View();
        }
        [HttpPost]
        public IActionResult AddBill(Bill bill)
        {
            _dbContext.Add(bill);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditBill(int? id)
        {
            ViewBag.Accountants = new SelectList(_dbContext.Accountants, "Accountantid", "Accountantname");
            ViewBag.Patients = new SelectList(_dbContext.Patients, "Patientid", "Patientname");
            if (id == null) { return NotFound(); }
            var bill = _dbContext.Bills.Find(id);
            if (bill == null) { return NotFound(); }
            return View(bill);
        }
        [HttpPost]
        public IActionResult EditBill(int id, Bill bill)
        {
            if (id != bill.Billid)
            { return NotFound(); }
            else
            {
                _dbContext.Update(bill);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteBill(int? id)
        {

            var bill = _dbContext.Bills
                .Include(b => b.Pat)
                .Include(b => b.Acc)
                .FirstOrDefault(B => B.Billid == id);
            if (bill == null) { return NotFound(); }
            return View(bill);
        }

        [HttpPost]
        public IActionResult DeleteBill(int id)
        {
            var bill = _dbContext.Bills.Find(id);
            _dbContext.Bills.Remove(bill);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Medical Record
        public IActionResult AddRecord()
        {
            ViewBag.Patients = new SelectList(_dbContext.Patients, "Patientid", "Patientname");
            return View();
        }
        [HttpPost]
        public IActionResult AddRecord(Medicalrecord medicalrecord)
        {
            _dbContext.Add(medicalrecord);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditRecord(int? id)
        {

            ViewBag.Patients = new SelectList(_dbContext.Patients, "Patientid", "Patientname");
            if (id == null) { return NotFound(); }
            var record = _dbContext.Medicalrecords.Find(id);
            if (record == null) { return NotFound(); }
            return View(record);
        }
        [HttpPost]
        public IActionResult EditRecord(int id, Medicalrecord medicalrecord)
        {
            if (id != medicalrecord.Recordid)
            { return NotFound(); }
            else
            {
                _dbContext.Update(medicalrecord);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult DeleteRecord(int? id)
        {

            var record = _dbContext.Medicalrecords
                .Include(b => b.Pat)
                .FirstOrDefault(M => M.Recordid == id);
            if (record == null) { return NotFound(); }
            return View(record);
        }
        [HttpPost]
        public IActionResult DeleteRecord(int id)
        {
            var record = _dbContext.Medicalrecords.Find(id);
            _dbContext.Medicalrecords.Remove(record);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
