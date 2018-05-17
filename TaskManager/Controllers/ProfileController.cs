using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Helper;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    [SecurityAuthorization(Roles = "Admin")]
    public class ProfileController : Controller
    {

        // GET: Profile
        public ActionResult Index()
        {
            var profileRepo = new ProfileRepository();

            var profileList = profileRepo.GetAll();
            return View("Index", profileList);
        }

        public ActionResult Edit(long? id)
        {
            var profileRepo = new ProfileRepository();
            Profile profile = null;
            if (id != null)
                profile = profileRepo.GetById(id ?? default(long));

            return View("Edit", profile);
        }

        [HttpPost]
        public ActionResult Save(Profile profile)
        {
            var profileRepo = new ProfileRepository();


            Boolean isInsert = profile.Id == 0L;

            if (isInsert)
            {
                //Verifica no banco se já tem um email igual
                var profileInDB = profileRepo.GetByEmail(profile.Email);

                //Se existir um e-mail igual e tiver desativado só atribui o id para o objeto e ele irá atualizar para ativo
                if (profileInDB != null && profileInDB.IsActive == false)
                    profile.Id = profileInDB.Id;
            }
            try
            {
                /**
                 * Adiciona o perfil 2 que é dos participantes dos times, 
                 * como só pode ter um admin, não tem como escolher esse perfil**/

                var role = new Role { Id = 2 };
                profile.IsActive = true;
                profile.Role = role;

                profileRepo.EditOrCreate(profile);
                return RedirectToAction("Index");
            }
            catch (SqlException)
            {
                ModelState.AddModelError("Email", "Email duplicado!");
                return View("Edit", profile);
            }
        }

        public ActionResult Delete(long id)
        {
            var profileRepo = new ProfileRepository();
            profileRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }

}