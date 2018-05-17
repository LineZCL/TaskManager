using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Tests.Repository
{
    [TestClass]
    public class ProfileRepositoryTest
    {

        private RoleRepository roleRepo;
        private Role role;

        private ProfileRepository profileRepo;

        private List<Profile> profiles;

        [TestInitialize]
        public void TestInitialize()
        {
            roleRepo = new RoleRepository();
            profileRepo = new ProfileRepository();


            profiles = new List<Profile>();

          
            role = new Role { Description = "Team", IsActive = true };
            role = roleRepo.EditOrCreate(role);

        }

        [TestCleanup]
        public void Clean()
        {
            foreach (var profile in profiles)
                profileRepo.DeleteDatabase(profile.Id);
            roleRepo.DeleteDatabase(role.Id);
        }

        [TestMethod]
        public void CreateProfileSuccess()
        {

            var profile = new Profile
            {
                Name = "Maria",
                Email = "maria@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = true
            };

            profiles.Add(profileRepo.EditOrCreate(profile));
            var profileInDatabase = profileRepo.GetById(profiles[0].Id);

            Assert.IsNotNull(profileInDatabase);

        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void CreateProfileFailNameIsNull()
        {
            var profile = new Profile
            {
                Email = "maria@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = true
            };

            profiles.Add(profileRepo.EditOrCreate(profile));
        }
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void CreateProfileFailEmailDouble()
        {
            var profile = new Profile
            {
                Name = "Maria",
                Email = "maria@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = true
            };

            profiles.Add(profileRepo.EditOrCreate(profile));

            var profile2 = new Profile
            {
                Name = "Maria",
                Email = "maria@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = true
            };

            profiles.Add(profileRepo.EditOrCreate(profile2));
        }
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void CreateProfileFailEmailIsNull()
        {
            var profile = new Profile
            {
                Name = "Maria",
                Password = "12345678",
                Role = role,
                IsActive = true
            };

            profiles.Add(profileRepo.EditOrCreate(profile));

        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void CreateProfileFailPasswordIsNull()
        {
            var profile = new Profile
            {
                Name = "Maria",
                Email = "maria@teste123.com.br",
                Role = role,
                IsActive = true
            };

            profiles.Add(profileRepo.EditOrCreate(profile));

        }

        [TestMethod]
        public void GetAllProfileSuccess()
        {
            var profile = new Profile
            {
                Name = "Maria",
                Email = "maria@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = true
            };
            profile = profileRepo.EditOrCreate(profile); 
            profiles.Add(profile);

            var profilesInDb = profileRepo.GetAll();
            Assert.IsNotNull(profilesInDb[0].Role);

        }

        [TestMethod]
        public void GetProfileByIdSuccess()
        {
            var profile = new Profile
            {
                Name = "Maria",
                Email = "maria@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = true
            };
            profile = profileRepo.EditOrCreate(profile);
            profiles.Add(profile);

            var profileInDb = profileRepo.GetById(profile.Id);
            Assert.IsNotNull(profileInDb.Role);

        }

        [TestMethod]
        public void GetByEmailWithResult()
        {
            var profile = new Profile
            {
                Name = "Maria",
                Email = "maria@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = true
            };
            profile = profileRepo.EditOrCreate(profile);
            profiles.Add(profile);

            var profileInDb = profileRepo.GetByEmail("maria@teste123.com.br");
            Assert.IsNotNull(profileInDb);
        }
        [TestMethod]
        public void GetByEmailNotfound()
        {
            var profile = new Profile
            {
                Name = "Maria",
                Email = "maria@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = true
            };
            profile = profileRepo.EditOrCreate(profile);
            profiles.Add(profile);

            var profileInDb = profileRepo.GetByEmail("maria@teste123.com.brd");
            Assert.IsNull(profileInDb);
        }
    }
}
