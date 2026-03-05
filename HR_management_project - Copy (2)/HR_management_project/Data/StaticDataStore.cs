namespace HR_management_project.Data
{
    public class StaticDataStore : IDataStore
    {
        private readonly Dictionary<string, object> _dataStore
            = new Dictionary<string, object>();

        public Task<T> Add<T>(T data)
        {
            var typeKey = typeof(T).Name;

            if (!_dataStore.ContainsKey(typeKey))
            {
                _dataStore[typeKey] = new List<T>();
            }

            var list = (List<T>)_dataStore[typeKey];
            list.Add(data);

            return Task.FromResult(data);
        }

        public Task<List<T>> GetList<T>()
        {
            var typeKey = typeof(T).Name;

            if (_dataStore.TryGetValue(typeKey, out var value) && value is List<T> list)
            {
                return Task.FromResult(list);
            }

            return Task.FromResult(new List<T>());
        }

        public Task<T> GetData<T>(string key)
        {
            if (_dataStore.TryGetValue(key, out var value) && value is T typedValue)
            {
                return Task.FromResult(typedValue);
            }

            throw new KeyNotFoundException($"No data found for key: {key}");
        }

        public Task Delete<T>(T data)
        {
            var typeKey = typeof(T).Name;

            if (_dataStore.TryGetValue(typeKey, out var value) && value is List<T> list)
            {
                if (list.Remove(data))
                    return Task.CompletedTask;
            }

            throw new KeyNotFoundException($"Item not found for type: {typeKey}");
        }
    }
}
