using Hospital.BLL.Interfaces;
using Hospital.DAL.Context;
using Hospital.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.BLL.Repository
{
    public class DepartmentRepository : IGenericRepository<Depatment>
    {
        private readonly HospitalDbContext _dbContext;

        public DepartmentRepository(HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Depatment depatment)
        {
            _dbContext.Add(depatment);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var depatment = _dbContext.Depatments.Where(P => P.Departmentid == id).FirstOrDefault();
            _dbContext.Depatments.Remove(depatment);
            return _dbContext.SaveChanges();
        }

        public Depatment Get(int id)
        {
            var department = _dbContext.Depatments
                .Include(d => d.Wards)
                .FirstOrDefault(d => d.Departmentid == id);
            return department;
        }

        public IEnumerable<Depatment> GetAll() => _dbContext.Depatments.ToList();

        public int Update(Depatment depatment)
        {
            _dbContext.Depatments.Update(depatment);
            return _dbContext.SaveChanges();
        }
    }
}
