using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Tests.Repository
{
    [TestClass]
    public class TaskRepositoryTest
    {
        private RoleRepository roleRepo;
        private Role role;

        private ProfileRepository profileRepo;
        private Profile profile;

        private TaskRepository taskRepository;
        private List<Task> tasks; 

        [TestInitialize]
        public void TestInitialize()
        {
            roleRepo = new RoleRepository();
            profileRepo = new ProfileRepository();
            taskRepository = new TaskRepository(); 

            tasks = new List<Task>();

            role = new Role { Description = "Team", IsActive = true };
            role = roleRepo.EditOrCreate(role);

            profile = new Profile
            {
                Name = "Maria",
                Email = "maria@teste123.com.br",
                Password = "12345678",
                Role = role,
                IsActive = true
            };
            profile = profileRepo.EditOrCreate(profile);
            
        }

        [TestCleanup]
        public void Clean()
        {
            foreach (var task in tasks)
                taskRepository.DeleteDatabase(task.Id);

            profileRepo.DeleteDatabase(profile.Id);
            roleRepo.DeleteDatabase(role.Id);
        }

        [TestMethod]
        public void createTaskSuccess()
        {
            var task = new Task {
                Description = "Task 1",
                Sponsor = profile,
                Status = TaskStatus.PENDING,
                Date = DateTime.Now,
                IsActive = true
            };

            task = taskRepository.EditOrCreate(task);

            tasks.Add(task);

            var taskInDatabase = taskRepository.GetById(task.Id);
            Assert.IsNotNull(taskInDatabase);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void createTaskFailDescriptionNull()
        {
            var task = new Task
            {
                Sponsor = profile,
                Status = TaskStatus.PENDING,
                Date = DateTime.Now,
                IsActive = true
            };          
            tasks.Add(taskRepository.EditOrCreate(task));          
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void createTaskFailStatusNull()
        {
            var task = new Task
            {
                Description = "Task 1",
                Sponsor = profile,
                Date = DateTime.Now,
                IsActive = true
            };
            tasks.Add(taskRepository.EditOrCreate(task));
        }

        [TestMethod]
        [ExpectedException(typeof(SqlTypeException))]
        public void createTaskFailDateNull()
        {
            var task = new Task
            {
                Description = "Task 1",
                Sponsor = profile,
                Status = TaskStatus.PENDING,
                IsActive = true
            };
            tasks.Add(taskRepository.EditOrCreate(task));
        }

    }
}
