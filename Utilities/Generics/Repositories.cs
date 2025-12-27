using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Utilities.Generics
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Task ve async: İşlemlerin asenkron (eşzamanlı/paralel) olarak gerçekleştirilmesini sağlayan yapıdır. Dikkat edilmesi gereken en önemli durum bu yapıyla başlayan süreci yine bu yapıyla devam ettirmektir.
        // Task<T>: Return type olarak kullanılır, belirli bir işlemin sonucunu temsil eden bir yapıdır. Asenkron işlemler için kullanılır.
        // async: Task<T> return type metotlarda, metodun asenkron olarak çalışacağını belirtir. Bu, metodun çağrıldığı yerde beklenmeden diğer işlemlerin devam etmesini sağlar.
        // await: async olarak işaretlenmiş bir metodun içinde, başka bir asenkron işlemi beklemek için kullanılır. Bu, işlemin tamamlanmasını beklerken diğer işlemlerin devam etmesini sağlar.
        // Kozmetik olarak bu tip metotların sonuna "Async" eklenebilir. Örneğin: CreateAsync, FindByIdAsync gibi.

        // Task -> void gibi düşünülebilir. Yani geriye bir değer döndürmez.
        Task CreateAsync(TEntity entity);
        Task CreateManyAsync(IEnumerable<TEntity> entities);

        // Task<T> -> T gibi düşünülebilir. Yani geriye T tipinde bir değer döndürür.
        Task<TEntity?> FindByIdAsync(int id);
        Task<TEntity?> FindFirstAsync(Expression<Func<TEntity, bool>>? predicate = null, params string[] includes);
        Task<IQueryable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>>? predicate = null, params string[] includes);

        Task UpdateAsync(TEntity entity);
        Task UpdateManyAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);
        Task DeleteManyAsync(IEnumerable<TEntity> entities);

        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null);
    }

    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        //predicate: Koşul belirlemek için kullanılan bir fonksiyon. Örneğin, belirli bir özelliğe sahip varlıkları filtrelemek için kullanılabilir. SQL tarafında WHERE koşuluna benzer.
        // Func<TEntity, bool>: TEntity tipinde bir girdi alır ve bool tipinde bir çıktı döndürür. Yani, bir varlığın belirli bir koşulu sağlayıp sağlamadığını kontrol etmek için kullanılır.
        // Şablonu: x => x.Property == value && x.OtherProperty > otherValue || ... gibi.
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null
                ? await _dbSet.AnyAsync()
                : await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task CreateManyAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            // Asenkron olmayan metotlar için Task sınıfından Run metodu kullanılabilir. Bu sayede metot asenkron hale getirilir.
            await Task.Run(() => _dbSet.Remove(entity));
        }

        public async Task DeleteManyAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _dbSet.RemoveRange(entities));
        }

        public async Task<TEntity?> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity?> FindFirstAsync(Expression<Func<TEntity, bool>>? predicate = null, params string[] includes)
        {
            IQueryable<TEntity> query = predicate == null ? _dbSet : _dbSet.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IQueryable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>>? predicate = null, params string[] includes)
        {
            IQueryable<TEntity> query = predicate == null ? _dbSet : _dbSet.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await Task.Run(() => query);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _dbSet.Update(entity));
        }

        public async Task UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _dbSet.UpdateRange(entities));
        }
    }
}
