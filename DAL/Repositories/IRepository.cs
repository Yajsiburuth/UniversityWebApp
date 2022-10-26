using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Find(int id);
        int Create(T entity);
        void Update(T entity);

    }
}
