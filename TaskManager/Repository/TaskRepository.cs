using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class TaskRepository : BaseRepository<Task>
    {

        public override List<Task> GetAll()
        {
            var tasks = base.GetAll(); 
            using(Database db = new Database(CONNECTION_STRING_NAME))
            {
                foreach(var task in tasks)
                {
                    string sql = "SELECT p.* FROM Task t INNER JOIN Profile p ON p.Id = t.SponsorId " +
                       "WHERE t.id = @0";
                    var sponsor = db.SingleOrDefault<Profile>(sql, task.Id);
                    task.Sponsor = sponsor;

                }
            }
            return tasks;
        }

        public override Task GetById(long id)
        {
            var task = base.GetById(id);
            if (task != null)
            {
                using (Database db = new Database(CONNECTION_STRING_NAME))
                {
                    string sql = "SELECT p.* FROM Task t INNER JOIN Profile p ON p.Id = t.SponsorId " +
                          "WHERE t.id = @0";
                    var sponsor = db.SingleOrDefault<Profile>(sql, task.Id);
                    task.Sponsor = sponsor;

                    return task;
                }
            }
            else
            {
                return null;
            }
        }
    }
}