using HR_management_project.Data.Core;
using HR_management_project.Data.Stores.SqlLiteStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Factory
{
    internal class DataStoreFactory
    {
        public static IDataStore CreateDataStore(ApplicationDbContext dbContext)
        { 
            return new DataBaseStore(dbContext);
        }
    }
}
