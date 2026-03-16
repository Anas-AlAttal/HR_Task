using System.Linq.Expressions;

namespace HR_management_project.Data
{
    public interface IDataStore
    {
        Task<T> Add<T>(T data) where T : class, IEntity<int>;
        Task Delete<T>(T data) where T : class;
        Task<TEntity> GetData<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>;
        Task<List<T>> GetList<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        
        Task Update<TEntity, TKey>(TEntity data) where TEntity : class, IEntity<TKey>;
       

    }
}
