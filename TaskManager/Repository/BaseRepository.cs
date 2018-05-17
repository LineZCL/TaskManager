using NPoco;
using System;
using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {

        protected const String CONNECTION_STRING_NAME = "CSTaskManager";

        public virtual List<T> GetAll()
        {
            using (Database db = new Database(CONNECTION_STRING_NAME))
            {
                return db.Fetch<T>("WHERE IsActive = @0", true);
            }
        }

        public virtual T GetById(long id)
        {
            using (Database db = new Database(CONNECTION_STRING_NAME))
            {
                return db.SingleOrDefaultById<T>(id);
            }

        }

        public virtual T EditOrCreate(T model)
        {
            using (Database db = new Database(CONNECTION_STRING_NAME))
            {
                if (model.Id == 0L)
                {
                    db.Insert<T>(model);
                }
                else
                {
                    db.Update(model);
                }
                return model;
            }
        }

        public virtual void Delete(long id)
        {
            T model = GetById(id);
            Delete(model);
        }

        public virtual void Delete(T model)
        {
            if (model != null)
            {
                using (Database db = new Database(CONNECTION_STRING_NAME))
                {
                    model.IsActive = false;
                    db.Update(model);
                }
            }
        }

        public virtual void DeleteDatabase(long id)
        {
            using(Database db = new Database(CONNECTION_STRING_NAME))
            {
                db.Delete<T>(id);
            }
        }


    }
}