using Hospital.DAL.Models;

namespace Hospital.PL.Models
{
    public class PatientDetailsViewModel
    {
        //public Patient Patient { get; set; }
        //public Doctor Doctor { get; set; }
        public IEnumerable<Bill> Bills { get; set; }
        public IEnumerable<Medicalrecord> MedicalRecords { get; set; }
        public IEnumerable<Doctorwritetestreport> DoctorWriteTestReports { get; set; }
        public IEnumerable<Testreporttreatment> Testreporttreatments { get; set; }
    }
}

