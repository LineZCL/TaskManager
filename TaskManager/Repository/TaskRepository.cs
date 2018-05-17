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
            //var tasks = base.GetAll(); 
            using (Database db = new Database(CONNECTION_STRING_NAME))
            {
                //foreach(var task in tasks)
                //{
                //    string sql = "SELECT p.* FROM Task t INNER JOIN Profile p ON p.Id = t.SponsorId " +
                //       "WHERE t.id = @0";
                //    var sponsor = db.SingleOrDefault<Profile>(sql, task.Id);
                //    task.Sponsor = sponsor;

                //}
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
                string sql = "SELECT t.*, p.* FROM Task t INNER JOIN Profile p ON p.id = t.SponsorId WHERE p.id = @0";
                return db.Fetch<Task>(sql, sponsorId);

            }
        }
    }
}