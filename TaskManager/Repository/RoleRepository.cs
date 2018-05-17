using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class RoleRepository : BaseRepository<Role>
    {
        public Role getTeam()
        {
            using(Database db = new Database(CONNECTION_STRING_NAME))
            {
                return db.SingleOrDefault<Role>("WHERE Description LIKE 'Team'");
            }
        }
    }
}