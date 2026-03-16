using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Data
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
