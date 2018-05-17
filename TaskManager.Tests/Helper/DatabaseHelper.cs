using System;
using System.Collections.Generic;

using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Tests.Helper
{
    class DatabaseHelper
    {
        public static void CleanDatabase()
        {
            var profileRepo = new ProfileRepository();
            var taskRepo = new TaskRepository();
            var roleRepo = new RoleRepository();
            var taskDependencyRepo = new TaskDependencyRepository();

            DeleteTaskDependency(taskDependencyRepo);
            DeleteTask(taskRepo);
            DeleteProfile(profileRepo);
            DeleteRole(roleRepo);
        }

        private static void DeleteTaskDependency(TaskDependencyRepository taskDependencyRepo)
        {
            var list = taskDependencyRepo.GetAllData();

            foreach(var item in list){
                taskDependencyRepo.DeleteDatabase(item.Id);
            }
       
        }

        private static void DeleteTask(TaskRepository taskRepository)
        {
            var list = taskRepository.GetAllData();

            foreach (var item in list)
            {
                taskRepository.DeleteDatabase(item.Id);
            }

        }

        private static void DeleteProfile(ProfileRepository profilerRepo)
        {
            var list = profilerRepo.GetAllData();

            foreach (var item in list)
            {
                profilerRepo.DeleteDatabase(item.Id);
            }

        }

        private static void DeleteRole(RoleRepository roleRepo)
        {
            var list = roleRepo.GetAllData();

            foreach (var item in list)
            {
                roleRepo.DeleteDatabase(item.Id);
            }

        }

    }
}
