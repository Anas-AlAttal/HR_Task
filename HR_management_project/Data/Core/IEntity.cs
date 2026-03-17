using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Data.Core
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
