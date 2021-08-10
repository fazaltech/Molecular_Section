using Molecular_Section.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Molecular_Section.Controllers
{
    public class RegistrationController : Controller
    {
        private Path_Molecular db = new Path_Molecular();
        // GET: Registration
        public ActionResult Index(string name, string role)
        {
            var Lst = new List<string>();

            var Qry = from d in db.user_dash

                      select d.role;
            Lst.AddRange(Qry.Distinct());
            ViewBag.roles = new SelectList(Lst);




            var prctv = from m in db.user_dash

                        select m;
            if (!String.IsNullOrEmpty(role))
            {
                prctv = prctv.Where(s => s.role.Contains(role));
            }
            if (!string.IsNullOrEmpty(name))
            {
                prctv = prctv.Where(x => x.user_name == name);
            }




            return View(prctv);
        }



        public ActionResult Login()
        {


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user_dash user)
        {
            var assgrole = db.user_dash
                 .Where(x => x.user_name == user.user_name)
                 .Where(x => x.password == user.password)
                 .Select(x => x.role).Max();


            if (assgrole == "assign role")
            {
                ViewBag.assgnRole = "Contact admin to assign role";
                return View(user);

            }

            else if (IsValid(user.user_name, user.password))
            {
                var mail = db.user_dash
                  .Where(x => x.user_name == user.user_name)
                  .Where(x => x.password == user.password)
                  .Select(x => x.email_id).Max();
                string m = mail;







                FormsAuthentication.SetAuthCookie(user.user_name, false);


                return RedirectToAction("specimen_index", "Forms");


            }
            else
            {


                ViewBag.Mg = "Login details are wrong.";
                return View(user);

            }


        }

        private bool IsValid(string name, string passwords)
        {

            bool IsValid = false;


            var user = db.user_dash.FirstOrDefault(u => u.user_name == name);
            if (user != null)
            {
                if (user.password == passwords)
                {
                    IsValid = true;
                }
            }

            return IsValid;
        }



        public ActionResult Logout(string returnUrl = null)
        {

            FormsAuthentication.SignOut();

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Login", "Registration");

            return Redirect(returnUrl);
        }

        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(user_dash use)
        {
            Thread.Sleep(200);
            var precheck = db.user_dash.Where(x => x.user_name == use.user_name).FirstOrDefault();

            if (precheck != null)
            {
                ViewBag.chk = "User Already Exist";
                return View(use);

            }
            else if (ModelState.IsValid)
            {

                use.role = "assign role";
                db.user_dash.Add(use);
                db.SaveChanges();


            }
            ViewBag.Message = "Contact admin to assign role";
            return View();
        }
    }
}