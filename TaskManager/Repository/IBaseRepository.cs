using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Repository
{
    interface IBaseRepository<T>
    {
        List<T> GetAll();

        T GetById(long id);

        T EditOrCreate(T model);

        void Delete(long id);

        void Delete(T model);

        void DeleteDatabase(long id);
    }
}
