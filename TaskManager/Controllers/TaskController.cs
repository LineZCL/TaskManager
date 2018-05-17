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
            return View(tasks);
        }

        public ActionResult Edit(long? id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            string roleConnected = CookieHelper.GetInfoUserConnected(identity, "role");

            var taskRepo = new TaskRepository();
            Task task = null;
            if (id != null)
                task = taskRepo.GetById(id ?? default(long));

            List<Task> candidatesTaskDependencies = null;
            var profileRepo = new ProfileRepository();

            if (roleConnected.Equals("Admin"))
            {
                var profiles = profileRepo.GetAll();
                ViewBag.profiles = profiles;

                candidatesTaskDependencies = taskRepo.GetAll();
            }
            else
            {
                var myProfile = profileRepo.GetByEmail(CookieHelper.GetInfoUserConnected(identity, "email"));
                if (myProfile != null)
                    candidatesTaskDependencies = taskRepo.GetBySponsorId(myProfile.Id);
            }

            ViewBag.candidatesTaskDependecies = candidatesTaskDependencies;

            return View(task);
        }

        [HttpPost]
        public ActionResult Save(Task task)
        {
            var taskRepo = new TaskRepository();
            task.IsActive = true;

            var profileRepo = new ProfileRepository();
            try
            {
                task.Sponsor = profileRepo.GetById(task.SponsorId);
                taskRepo.EditOrCreate(task);
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
    }
}