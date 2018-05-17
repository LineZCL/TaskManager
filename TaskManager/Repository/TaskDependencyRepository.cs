using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class TaskDependencyRepository : BaseRepository<TaskDependency>
    {
        public List<TaskDependency> GetByTaskId(long taskId)
        {
            using(Database db = new Database(CONNECTION_STRING_NAME))
            {
                string sql = "SELECT t.*, td.*, sb.* FROM TaskDependency td INNER JOIN Task t ON td.TaskId = t.Id INNER JOIN Task sb ON td.PendingTaskId = sb.Id WHERE t.id = @0";
                return db.Fetch<TaskDependency>(sql, taskId);
            }
        }  

        public void DeleteAllInTask(long taskId)
        {
            using (Database db = new Database(CONNECTION_STRING_NAME))
            { 
                var subTasks = db.Fetch<TaskDependency>("WHERE taskId = @0", taskId);

                foreach (var subTask in subTasks)
                {
                    db.Delete<TaskDependency>(subTask.Id);
                }
            }
        }
    }
}