using HR_management_project.Data.Core;
using System.Linq.Expressions;

namespace HR_management_project.Data.Stores.StaticStore
{
    public class StaticDataStore : IDataStore
    {
        private readonly Dictionary<string, object> _dataStore = new Dictionary<string, object>();
        private readonly Dictionary<string, int> _counter = new Dictionary<string, int>();


        public Task<T> Add<T>(T data) where T : class, IEntity<int>
        {
            var typeKey = typeof(T).Name;
            if (!_dataStore.ContainsKey(typeKey))
            {
                _dataStore[typeKey] = new List<T>();
            }
            if (!_counter.ContainsKey(typeKey))
                _counter[typeKey] = 1;

            data.Id = (int)(object)_counter[typeKey]++;

            var list = (List<T>)_dataStore[typeKey];
            if (list == null)
                throw new NullReferenceException(typeKey);

            list.Add(data);
            Console.WriteLine($"{{{data}}} added to database");
            return Task.FromResult(data);
        }

        public Task<List<T>> GetList<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            var typeKey = typeof(T).Name;
            if (_dataStore.TryGetValue(typeKey, out var value) && value is List<T> list)
            {
                return Task.FromResult(list);
            }
            return Task.FromResult(new List<T>());

        }

        public Task<TEntity> GetData<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>
        {
            var typeKey = typeof(TEntity).Name;
            if (_dataStore.TryGetValue(typeKey, out var value) && value is List<TEntity> list)
            {
                var entity = list.FirstOrDefault(e => e.Id!.Equals(key));
                if (entity != null)
                    return Task.FromResult(entity);
            }
            throw new NotImplementedException();

        }
        public Task Delete<T>(T data) where T : class
        {
            var typeKey = typeof(T).Name;
            if (_dataStore.TryGetValue(typeKey, out var value) && value is List<T> list)
            {
                if (list.Remove(data))
                {
                    Console.WriteLine($"data {{{data}}} deleted");
                    return Task.CompletedTask;
                }
            }

            throw new KeyNotFoundException($"No data found for type: {typeKey}");
        }

        public Task Update<TEntity, TKey>(TEntity data) where TEntity : class, IEntity<TKey>
        {
            var typeKey = typeof(TEntity).Name;
            if (_dataStore.TryGetValue(typeKey, out var value) && value is List<TEntity> list)
            {
                var index = list.FindIndex(e => e.Id!.Equals(data.Id));
                if (index != -1)
                {
                    list[index] = data;
                    Console.WriteLine($"data {{{data}}} updated");
                    return Task.CompletedTask;
                }

            }
            throw new KeyNotFoundException($"Entity with id {data.Id} not found.");
        }
    }
}
