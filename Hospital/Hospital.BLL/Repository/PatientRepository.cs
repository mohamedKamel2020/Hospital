
using Hospital.BLL.Interfaces;
using Hospital.DAL.Context;
using Hospital.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.BLL.Repository
{
    public class PatientRepository : IGenericRepository<Patient>
    {
        private readonly HospitalDbContext _dbContext;

        public PatientRepository(HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var patient = _dbContext.Patients.Where(P => P.Patientid == id).FirstOrDefault();
            _dbContext.Patients.Remove(patient);
            return _dbContext.SaveChanges();
        }
        public Patient Get(int id)
        {

            var patient = _dbContext.Patients
                .Include(p => p.Ward)
                .ThenInclude(w => w.DepatmentnoNavigation)
                .FirstOrDefault(p => p.Patientid == id);

            return patient;
        }

        public IEnumerable<Patient> GetAll()
        {
            var patients = _dbContext.Patients
               .Include(p => p.Ward)
               .ThenInclude(w => w.DepatmentnoNavigation)
               .ToList();
            return patients;
        }

        public int Update(Patient patient)
        {
            _dbContext.Patients.Update(patient);
            return _dbContext.SaveChanges();
        }
    }
}
