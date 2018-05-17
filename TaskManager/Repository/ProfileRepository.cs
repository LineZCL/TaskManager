using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class ProfileRepository : BaseRepository<Profile>
    {
        public override List<Profile> GetAll()
        {
            var profiles = base.GetAll();
            using (Database db = new Database(CONNECTION_STRING_NAME))
            {

                foreach (var profile in profiles)
                {
                    string sql = "SELECT r.* FROM Profile p INNER JOIN Role r ON p.RoleId = r.Id " +
                        "WHERE p.id = @0";
                    var role = db.SingleOrDefault<Role>(sql, profile.Id);
                    profile.Role = role;
                }

                return profiles;
            }
        }

        public override Profile GetById(long id)
        {
            var profile = base.GetById(id);
            if (profile != null)
            {
                using (Database db = new Database(CONNECTION_STRING_NAME))
                {
                    string sql = "SELECT r.* FROM Profile p INNER JOIN Role r ON p.RoleId = r.Id " +
                        "WHERE p.id = @0";
                    var role = db.SingleOrDefault<Role>(sql, profile.Id);
                    profile.Role = role;


                    return profile;
                }
            }
            return null;
        }

        public Profile GetByEmail(string email)
        {
            using (Database db = new Database(CONNECTION_STRING_NAME))
            {
                return db.SingleOrDefault<Profile>("WHERE Email LIKE @0", email);
            }
        }
    }
}