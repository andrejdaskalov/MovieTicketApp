using System.Collections.Generic;
using Domain;

namespace Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        public IEnumerable<T> GetAll();
        public T Get(int? id);
        public T Insert(T entity);
        public T Update(T entity);
        public T Delete(T entity);
    }
}