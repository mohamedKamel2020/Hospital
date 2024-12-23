using Hospital.DAL.Models;

namespace Hospital.PL.Models
{
    public class StaffViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Nurse> Nurses { get; set; }
        public IEnumerable<Accountant> Accountants { get; set; }
    }
}
