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
            using (Database db = new Database(CONNECTION_STRING_NAME))
            { 
                string sql = "SELECT t.*, p.* FROM Task t INNER JOIN Profile p ON p.id = t.SponsorId WHERE t.IsActive = 1";
                return db.Fetch<Task>(sql);

            }

        }

        public override Task GetById(long id)
        {
            using (Database db = new Database(CONNECTION_STRING_NAME))
            {
                string sql = "SELECT t.*, p.* FROM Task t INNER JOIN Profile p ON p.id = t.SponsorId WHERE t.id = @0 And t.isActive = 1";
                return db.SingleOrDefault<Task>(sql, id);
            }
        }

        public List<Task> GetBySponsorId(long sponsorId)
        {
            using (Database db = new Database(CONNECTION_STRING_NAME))
            {
                string sql = "SELECT t.*, p.* FROM Task t INNER JOIN Profile p ON p.id = t.SponsorId WHERE p.id = @0 AND t.isActive = 1";
                return db.Fetch<Task>(sql, sponsorId);
            }
        }

        public List<Task> GetSubtasks(long taskId)
        {
            using(Database db = new Database(CONNECTION_STRING_NAME))
            {
                string sql = "SELECT sb.* FROM TaskDependency td INNER JOIN Task t ON td.TaskId = t.Id INNER JOIN Task sb ON td.PendingTaskId = sb.Id WHERE t.id = @0";
                return db.Fetch<Task>(sql, taskId);
            }
        }
    }
}