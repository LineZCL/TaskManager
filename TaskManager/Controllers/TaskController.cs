using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var taskRepo = new TaskRepository();
            Task task = null;
            if (id != null)
                task = taskRepo.GetById(id ?? default(long));

            var profileRepo = new ProfileRepository();
            var profiles = profileRepo.GetAll();
            ViewBag.profiles = profiles;

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