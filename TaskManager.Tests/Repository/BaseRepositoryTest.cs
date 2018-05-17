using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Tests.Repository
{
    [TestClass]
    public class BaseRepositoryTest
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
        public void DeleteByIdSuccess()
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
            profileRepo.Delete(profile.Id);

            var profileInDatabase = profileRepo.GetById(profile.Id);
            Assert.IsFalse(profileInDatabase.IsActive);
 
        }

        [TestMethod]
        public void DeleteByIdNotExist()
        {
            profileRepo.Delete(0);
        }

        [TestMethod]
        public void DeleteByObjectSuccess()
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
            profileRepo.Delete(profile);

            var profileInDatabase = profileRepo.GetById(profile.Id);
            Assert.IsFalse(profileInDatabase.IsActive);

        }

        [TestMethod]
        public void DeleteByObjectNull()
        {
            profileRepo.Delete(0);
        }

        [TestMethod]
        public void getAllSuccess()
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

            var profileList = profileRepo.GetAll();
            Assert.AreEqual(1, profileList.Count);
        }

        [TestMethod]
        public void getAllEmpty()
        {
            var profileList = profileRepo.GetAll();
            Assert.IsNotNull(profileList);
            Assert.AreEqual(0, profileList.Count);
        }

        [TestMethod]
        public void getAllWith1DeletedSuccess()
        {
            var profileMaria = new Profile
            {
                Name = "Maria",
                Email = "maria@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = true
            };
            profiles.Add(profileRepo.EditOrCreate(profileMaria));

            var profileJoao = new Profile
            {
                Name = "Joao",
                Email = "joao@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = false
            };
            profiles.Add(profileRepo.EditOrCreate(profileJoao));

            var profileList = profileRepo.GetAll();
            Assert.AreEqual(1, profileList.Count);
        }
    }
}
