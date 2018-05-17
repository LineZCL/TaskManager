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
            string roleConnected = CookieHelper.GetInfoUserConnected(identity, "role");

            ViewBag.IsAdmin = roleConnected.Equals("Admin");
            ViewBag.Email = CookieHelper.GetInfoUserConnected(identity, "email");

            return View(tasks);
        }

        public ActionResult Edit(long? id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            string roleConnected = CookieHelper.GetInfoUserConnected(identity, "role");

            var taskRepo = new TaskRepository();
            Task task = null;
            List<long> subTasksIds = new List<long>();

            if (id != null)
            {
                task = taskRepo.GetById(id ?? default(long));
                var subTasks = taskRepo.GetSubtasks(task.Id);

                foreach (var subTask in subTasks)
                {
                    subTasksIds.Add(subTask.Id);
                }
            }
            ViewBag.SubTaskSelected = subTasksIds;


            List<Task> candidatesTaskDependencies = null;
            var profileRepo = new ProfileRepository();

            if (roleConnected.Equals("Admin"))
            {
                var profiles = profileRepo.GetAll();
                ViewBag.profiles = profiles;

                if (id == null)
                    candidatesTaskDependencies = taskRepo.GetAll();
                else
                    candidatesTaskDependencies = taskRepo.GetAll(task.Id);
            }
            else
            {
                var myProfile = profileRepo.GetByEmail(CookieHelper.GetInfoUserConnected(identity, "email"));
                if (myProfile != null)
                {
                    if (id == null)
                        candidatesTaskDependencies = taskRepo.GetBySponsorId(myProfile.Id);
                    else
                    {
                        candidatesTaskDependencies = taskRepo.GetBySponsorId(myProfile.Id, task.Id);
                    }
                }
            }

            ViewBag.candidatesTaskDependecies = candidatesTaskDependencies;

            return View(task);
        }

        [HttpPost]
        public ActionResult Save(Task task, List<long> TasksIds)
        {
            var taskRepo = new TaskRepository();
            task.IsActive = true;

            var profileRepo = new ProfileRepository();
            try
            {
                task.Sponsor = profileRepo.GetById(task.SponsorId);
                task = taskRepo.EditOrCreate(task);

                SaveSubTasks(task, TasksIds, taskRepo);
                return RedirectToAction("Index");
            }
            catch (SqlException)
            {
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
    }
}