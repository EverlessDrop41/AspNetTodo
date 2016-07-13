using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TodoList.EF.Contract.DTO;

namespace TodoList.EF.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> AsQueryable();
        bool Has(int id);
        AddDTO Create(T item);
        SuccessDTO Update(int id, Func<T, T> getNew);
        SuccessDTO Delete(int id);
    }
}
