using Hospital.BLL.Interfaces;
using Hospital.DAL.Context;
using Hospital.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.BLL.Repository
{
    public class WardRepository : IGenericRepository<Ward>
    {
        private readonly HospitalDbContext _dbContext;

        public WardRepository(HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Ward ward)
        {
            _dbContext.Add(ward);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var ward = _dbContext.Wards.Where(W => W.Wardid == id).FirstOrDefault();
            _dbContext.Wards.Remove(ward);
            return _dbContext.SaveChanges();
        }

        public Ward Get(int id)
        {
            var ward = _dbContext.Wards
                .Include(w => w.DepatmentnoNavigation)
                .Include(w => w.Patients)
                .FirstOrDefault(w => w.Wardid == id);
            return ward;
        }

        public IEnumerable<Ward> GetAll()
        {
            var wards = _dbContext.Wards
                 .Include(w => w.DepatmentnoNavigation)
                 .Include(w => w.Patients)
                 .ToList();
            return wards;
        }

        public int Update(Ward ward)
        {

            _dbContext.Wards.Update(ward);
            return _dbContext.SaveChanges();
        }
    }
}
