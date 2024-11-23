using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using PostaAPI.AppDbContext;

namespace PostaAPI.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            Save();
        }

        public T AddAndGetEntity(T entity)
        {
            _dbSet.Add(entity);
            Save();
            return entity;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<T> FindByCriteria(Func<T, bool> predicate, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null && includes.Length > 0)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(predicate).ToList();
        }
        public T GetByIdWithNavigations(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault(e => EF.Property<int>(e, "Id") == id);
        }
        public IEnumerable<T> GetAllWithNavigations(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        public async Task<IEnumerable<T>> FindByCriteriaAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                int count = includes.Length;
                for (int index = 0; index < count; index++)
                {
                    query = query.Include(includes[index]);
                }
            }

            return query.AsNoTracking().Where(predicate).ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

    }
}
