using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TaskManager.Helper;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            var taskRepo = new TaskRepository();

            var tasks = taskRepo.GetAll();

            var identity = (ClaimsIdentity)User.Identity;
           
            ViewBag.IsAdmin = GetRole().Equals("Admin");
            ViewBag.Email = CookieHelper.GetInfoUserConnected(identity, "email");

            return View(tasks);
        }

        public ActionResult Edit(long? id)
        {
            var identity = (ClaimsIdentity)User.Identity;

            var taskRepo = new TaskRepository();
            Task task = null;
            

            if (id != null)
            {
                task = taskRepo.GetById(id ?? default(long));
            }

            String role = GetRole();
            ViewBag.IsAdmin = role.Equals("Admin");
            GenerateViewBagSponser(id ?? default(long), role);
            GenerateViewBagSubTask(id ?? default(long), taskRepo, role);

            return View(task);
        }

        [HttpPost]
        public ActionResult Save(Task task, List<long> TasksIds, long? SponsorId)
        {
            var taskRepo = new TaskRepository();
            TaskStatusHelper taskStatusHelper = new TaskStatusHelper(taskRepo);

            var profileRepo = new ProfileRepository();
            task.IsActive = true;
            if (SponsorId != null)
            {
                task.Sponsor = profileRepo.GetById(SponsorId ?? default(long));
            }
            else
            {
                task.Sponsor = GetUserLogged();
            }

            string error = taskStatusHelper.validateStatusChange(task);
            if (error == null || error.Equals(""))
            {
                try
                {
                    task = taskRepo.EditOrCreate(task);
                    if (TasksIds != null)
                    {
                        SaveSubTasks(task, TasksIds, taskRepo);
                    }
                    return RedirectToAction("Index");
                }
                catch (SqlException)
                {
                    return View("edit", task);
                }
            }
            else
            {
                string role = GetRole();
                ModelState.AddModelError("Status" , error);
                ViewBag.IsAdmin = role.Equals("Admin");
                GenerateViewBagSponser(task.Id, role);
                GenerateViewBagSubTask(task.Id, new TaskRepository(), role);
                return View("edit", task);
            }
        }

        public ActionResult Delete(long id)
        {
            var taskRepo = new TaskRepository();
            taskRepo.Delete(id);

            return RedirectToAction("Index");
        }

        private void SaveSubTasks(Task task, List<long> subTasksIds, TaskRepository taskRepo)
        {
            var taskDependencyRepo = new TaskDependencyRepository();
            taskDependencyRepo.DeleteAllInTask(task.Id);

            foreach (var subTaskId in subTasksIds)
            {
                var subTask = taskRepo.GetById(subTaskId);
                var subTaskDependency = new TaskDependency
                {
                    IsActive = true,
                    Task = task,
                    PendingTask = subTask
                };

                taskDependencyRepo.EditOrCreate(subTaskDependency);
            }
        }

        private Profile GetUserLogged()
        {
            var identity = (ClaimsIdentity)User.Identity;
            string email = CookieHelper.GetInfoUserConnected(identity, "email");

            var profileRepo = new ProfileRepository();
            return profileRepo.GetByEmail(email);

        }

        private String GetRole()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return CookieHelper.GetInfoUserConnected(identity, "role");

            

        }


        private void GenerateViewBagSponser(long taskId, string role)
        {
            var profileRepository = new ProfileRepository();
           if (role.Equals("Admin"))
            {
                var profiles = profileRepository.GetAll();
                ViewBag.profiles = profiles;
            }

        }

        private void GenerateViewBagSubTask(long taskId, TaskRepository taskRepo, String role)
        {
            var isInsert = taskId == 0;
            List<long> subTasksIds = new List<long>();
            var subTasks = taskRepo.GetSubtasks(taskId);
            foreach (var subTask in subTasks)
            {
                subTasksIds.Add(subTask.Id);
            }
            ViewBag.SubTaskSelected = subTasksIds;

            List<Task> candidatesSubTasks = null;
            
            if (role.Equals("Admin"))
            {
                if (isInsert)
                    candidatesSubTasks = taskRepo.GetAll();
                else
                    candidatesSubTasks = taskRepo.GetAll(taskId);
            }
            else
            {
                var userLogged = GetUserLogged();
                if (userLogged != null)
                {
                    if (isInsert)
                        candidatesSubTasks = taskRepo.GetBySponsorId(userLogged.Id);
                    else
                    {
                        candidatesSubTasks = taskRepo.GetBySponsorId(userLogged.Id, taskId);
                    }
                }
            }

            ViewBag.candidatesSubTasks = candidatesSubTasks;
        }
    }
}