using HR_management_project.Data.Core;
using HR_management_project.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace HR_management_project.Data.Stores.SqlLiteStore
{
    public class DataBaseStore : IDataStore 
    {
        private readonly ApplicationDbContext _dbContext;

        public DataBaseStore(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add<T>(T data) where T : class, IEntity<int>
        {
            
            await _dbContext.Set<T>().AddAsync(data);
            await _dbContext.SaveChangesAsync();
            return data;

        }
        public async Task Delete<T>(T data) where T: class
        {
            _dbContext.Set<T>().Remove(data);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<TEntity> GetData<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>
        {
         var entity = await _dbContext.Set<TEntity>().FindAsync(key);
            //if (entity == null) 
            //    throw new KeyNotFoundException($"Entity with id {key} not found.");

            return entity;
        }
        public async Task<List<T>> GetList<T>(Expression<Func<T,bool>> predicate = null) where T : class
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (predicate != null)
            query = query.Where(predicate);

            return await query.ToListAsync();
        }
        //public void PrintAllData() 
        //{
        //    Console.WriteLine("=== All Data in Database ===");

        //    var entityTypes = _dbContext.Model.GetEntityTypes();

        //    foreach (var entityType in entityTypes)
        //    {
        //        var clrType = entityType.ClrType;
        //        if (clrType.Name == "EmployeeSalary")
        //            continue;

        //        Console.WriteLine($"\nType: {clrType.Name}");

        //        var method = typeof(DbContext)
        //            .GetMethod("Set", Type.EmptyTypes)!
        //            .MakeGenericMethod(clrType);

        //        var dbSet = method.Invoke(_dbContext, null);

        //        var items = ((System.Collections.IEnumerable)dbSet!).Cast<object>().ToList();

        //        foreach (var item in items)
        //            Console.WriteLine(item);
        //    }

        //    Console.WriteLine("=== End of Data ===");
        //}
        public async Task Update<TEntity, TKey>(TEntity data) where TEntity : class, IEntity<TKey>
        {
           _dbContext.Set<TEntity>().Update(data);
            await _dbContext.SaveChangesAsync();
        }

        

    }
}