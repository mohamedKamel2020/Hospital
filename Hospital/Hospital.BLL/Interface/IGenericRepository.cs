namespace Hospital.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T Get(int id);
        public int Add(T item);
        public int Delete(int id);
        public int Update(T item);
    }
}
