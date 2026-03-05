using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Data
{
    public interface IDataStore
    {
        Task<T> GetData<T>(string key);
        Task<List<T>> GetList<T>();
        Task<T> Add<T>(T data);
        Task Delete<T>(T data);
    }
}
