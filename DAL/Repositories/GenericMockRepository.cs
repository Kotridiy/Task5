using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    class GenericMockRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        static private List<T> _items = new List<T>();

        public void Add(T entity)
        {
            entity.Id = _items.Count;
            _items.Add(entity);
        }

        public void Delete(T entity)
        {
            _items.Remove(entity);
        }

        public T Get(int id)
        {
            return _items.Find(item => item.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return _items.AsQueryable();
        }

        public void Update(T entity)
        {
            var item = Get(entity.Id);
            Delete(item);
            Add(entity);
        }
    }
}
