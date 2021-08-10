using Molecular_Section.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Molecular_Section.Controllers
{
    public class FormsController : Controller
    {

        private Path_Molecular db = new Path_Molecular();
        // GET: Forms
        [Authorize]
        public ActionResult Index()
        {
            var urlt = from m in db.idrl
                       select m;
            return View(urlt);
        }

        [Authorize]
        [HttpGet]

        public ActionResult molecular_section_fill()
        {

            return View();
        }

        [Authorize]
        [HttpPost, ActionName("molecular_section_fill")]

        [ValidateAntiForgeryToken]
        public ActionResult molecular_section_fill(idrl idrl)
        {
            string name = User.Identity.Name;
            if (ModelState.IsValid)
            {
                idrl.user_name = name;
                db.idrl.Add(idrl);
                db.SaveChanges();
                return Json(new { success = true, responseText = "Data Insert Successfully" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "Contact Developer Team" }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult IndexView()
        {


            try
            {
                var pro = (from d in db.idrl


                           select new
                           {
                               d.datesample,
                               d.timesample,
                               d.receivedby,
                               d.temloggerinclude,
                               d.tempcoleman,
                               d.samplereceipt,
                               d.rnaextractiondate,
                               d.realtimepcrn1ddmmyy,
                               d.realtimepcrn1gen,
                               d.ctvaluep1,
                               d.realtimepcrn2ddmmyy,
                               d.realtimepcrn2gen,
                               d.ctvaluep2,
                               d.realtimepcrnEgen,
                               d.realtimepcrn3Egen,
                               d.ctvaluep3,
                               d.target1gcl,
                               d.target2gcl,
                               d.target1limitgcl,
                               d.target2limitgcl,
                               d.target1pre_absen,
                               d.target2pre_absen,
                               d.flowm3day,
                               d.passqaqc,
                               d.waterquality,
                               d.watervalue,
                               d.collectmethod,
                               d.note,
                               d.user_name,

                           }).ToList();




                return Json(new { data = pro }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }
        }

        [Authorize]
        public ActionResult specimen_index()
        {
            var alert = TempData["alert"];
            var add = TempData["record"];

            if (add != null)
            {
                ViewBag.add = add.ToString();
            }

            if (alert != null)
            {
                ViewBag.alert = alert.ToString();
            }
            return View();
        }


        [HttpGet]
        public JsonResult siteid_view()
        {
            try
            {


                return Json(db.sites.Where(x => x.colflag == 0).Select(x => new
                {

                    siteid = x.site_id,
                    sitename = x.site_name
                }).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }
        }



        [HttpGet]
        public JsonResult specimen_view(int? siteid)
        {
            try
            {


                return Json(db.specimens.Where(x => x.colflag == 0 && x.site_id == siteid).Select(x => new
                {

                    id = x.id,
                    specimen = x.specimen
                }).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }
        }


        
        [HttpGet]
        public JsonResult specimen_table(int siteid, string specimen)
        {
            var tableview = (Object)null;
            try
            {


                if (specimen == "-1")
                {
                    var pro = (from d in db.specimens
                               where d.site_id == siteid
                               where d.colflag == 0
                               join u in db.idrl on d.specimen equals u.specimen into b
                               from z in b.DefaultIfEmpty()
                               select new
                               {
                                   d.id,
                                   d.site_id,
                                   d.specimen,
                                   z.datesample,
                                   z.receivedby

                               }).ToList();
                    tableview = pro;
                }


                else if (specimen != "-1")
                {
                    var pro = (from d in db.specimens
                               where d.specimen == specimen
                               where d.colflag == 0
                               join u in db.idrl on d.specimen equals u.specimen into b
                               from z in b.DefaultIfEmpty()

                               select new
                               {
                                   d.id,
                                   d.site_id,
                                   d.specimen,
                                   z.datesample,
                                   z.receivedby

                               }).ToList();
                    tableview = pro;
                }
                return Json(new { data = tableview }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }
        }

        [HttpPost]
        public JsonResult checkspecimen(string specimen)
        {
            Thread.Sleep(200);
            var precheck = db.idrl.Where(x => x.specimen == specimen).FirstOrDefault();
            if (precheck != null)
            {

                return Json(new { success = false, responseText = "Record Already Exist", status = 1 }, JsonRequestBehavior.AllowGet);
            }

            if (precheck == null)
            {
                return Json(new { success = true, responseText = "Insert Record", status = 0 }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { failure = false }, JsonRequestBehavior.AllowGet);
        }



        [Authorize]
        [HttpGet]

        public ActionResult molecular_section1_fill(string specimen)
        {
            var add = TempData["record"];
            var idchkspecimen = db.idrl.Where(x => x.specimen == specimen).FirstOrDefault();
            var spchkspecimen = db.specimens.Where(x => x.specimen == specimen).FirstOrDefault();

            if (add != null)
            {

                return RedirectToAction("specimen_index", "Forms", new { alert = "Record Added" });
            }




            if (specimen == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (spchkspecimen == null)
            {
                return HttpNotFound();
            }
            if (idchkspecimen != null)
            {
                TempData["alert"] = "Record Already Exist";

                return RedirectToAction("specimen_index", "Forms", new { alert = "Record Already Exist" });

            }


            TempData["specimen"] = specimen;

            return View();
        }

        [Authorize]
        [HttpPost, ActionName("molecular_section1_fill")]

        [ValidateAntiForgeryToken]
        public ActionResult molecular_section1_fill(idrl idrl, idrlvsection1 idrlsec1)
        {
            var specimenschk = TempData["specimen"];
            string specimens = specimenschk.ToString();
            var siteidchk = db.specimens.Where(x => x.specimen == specimens.ToString()).Select(x => x.site_id).Max();
            int siteid = siteidchk;

            string name = User.Identity.Name;
            if (ModelState.IsValid)
            {
                idrl.site_id = siteid;
                idrl.specimen = specimens;


                idrl.datesample = idrlsec1.v1datesample;
                idrl.timesample = idrlsec1.v1timesample;
                idrl.receivedby = idrlsec1.v1receivedby;
                idrl.temloggerinclude = idrlsec1.v1temloggerinclude;
                idrl.tempcoleman = idrlsec1.v1tempcoleman;
                idrl.samplereceipt = idrlsec1.v1samplereceipt;
                idrl.laboratoryremark = idrlsec1.v1laboratoryremark;
                idrl.rnaextractiondate = idrlsec1.v1rnaextractiondate;
                idrl.rnaremark = idrlsec1.v1rnaremarkcheck;


                //idrl.realtimepcrn1ddmmyy { get; set; }
                //idrl.realtimepcrn1gen { get; set; }
                //idrl.ctvaluep1 { get; set; }
                //idrl.n1gneremark { get; set; }
                //idrl.realtimepcrn2ddmmyy { get; set; }
                //idrl.realtimepcrn2gen { get; set; }
                //idrl.ctvaluep2 { get; set; }
                //idrl.n2gneremark { get; set; }
                //idrl.realtimepcrnEgen { get; set; }
                //idrl.realtimepcrn3Egen { get; set; }
                //idrl.ctvaluep3 { get; set; }
                //idrl.n_e_gneremark { get; set; }
                //idrl.target1gcl { get; set; }
                //idrl.target2gcl { get; set; }
                //idrl.target1limitgcl { get; set; }
                //idrl.target2limitgcl { get; set; }
                //idrl.target1pre_absen { get; set; }
                //idrl.target2pre_absen { get; set; }
                //idrl.flowm3day { get; set; }
                //idrl.passqaqc { get; set; }
                //idrl.waterquality { get; set; }
                //idrl.watervalue { get; set; }
                //idrl.collectmethod { get; set; }
                //idrl.note { get; set; }
                //idrl.user_name { get; set; }


                idrl.entry_date = DateTime.Now.ToString(("dd/MM/yyyy HH:mm:ss"));
                idrl.user_name = name;
                db.idrl.Add(idrl);
                db.SaveChanges();
                TempData["record"] = "Record added";
                return Json(new { success = true, responseText = "Data Insert Successfully" }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { success = false, responseText = "Contact Developer Team" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]

        public ActionResult molecular_section2_fill(string specimen)
        {
            var add = TempData["record"];
            var idchkspecimen = db.idrl.Where(x => x.specimen == specimen).FirstOrDefault();
            var spchkspecimen = db.specimens.Where(x => x.specimen == specimen).FirstOrDefault();

            if (add != null)
            {

                return RedirectToAction("specimen_index", "Forms", new { alert = "Record Added" });
            }




            if (specimen == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (spchkspecimen == null)
            {
                return HttpNotFound();
            }
            if (idchkspecimen != null)
            {
                TempData["alert"] = "Record Already Exist";

                return RedirectToAction("specimen_index", "Forms", new { alert = "Record Already Exist" });

            }


            TempData["specimen"] = specimen;

            return View();
        }

        [Authorize]
        [HttpPost, ActionName("molecular_section2_fill")]

        [ValidateAntiForgeryToken]
        public ActionResult molecular_section2_fill(idrl idrl, idrlvsection2 idrlsec2)
        {
            var specimenschk = TempData["specimen"];
            string specimens = specimenschk.ToString();
            var siteidchk = db.specimens.Where(x => x.specimen == specimens.ToString()).Select(x => x.site_id).Max();
            int siteid = siteidchk;

            string name = User.Identity.Name;
            if (ModelState.IsValid)
            {
                idrl.site_id = siteid;
                idrl.specimen = specimens;


                idrl.realtimepcrn1ddmmyy = idrlsec2.v2realtimepcrn1ddmmyy;
                idrl.realtimepcrn1gen = idrlsec2.v2realtimepcrn1gen;
                idrl.ctvaluep1 = idrlsec2.v2ctvaluep1;
                idrl.n1gneremark = idrlsec2.v2n1gneremark;
                idrl.realtimepcrn2ddmmyy = idrlsec2.v2realtimepcrn2ddmmyy;
                idrl.realtimepcrn2gen = idrlsec2.v2realtimepcrn2gen;
                idrl.ctvaluep2 = idrlsec2.v2ctvaluep2;
                idrl.n2gneremark = idrlsec2.v2n2gneremark;
                idrl.realtimepcrnEgen = idrlsec2.v2realtimepcrnEgen;
                idrl.realtimepcrn3Egen = idrlsec2.v2realtimepcrn3Egen;
                idrl.ctvaluep3 = idrlsec2.v2ctvaluep3;
                idrl.n_e_gneremark = idrlsec2.v2n_e_gneremark;

                idrl.entry_date = DateTime.Now.ToString(("dd/MM/yyyy HH:mm:ss"));
                idrl.user_name = name;
                db.idrl.Add(idrl);
                db.SaveChanges();
                TempData["record"] = "Record added";
                return Json(new { success = true, responseText = "Data Insert Successfully" }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { success = false, responseText = "Contact Developer Team" }, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        [HttpGet]

        public ActionResult molecular_section3_fill(string specimen)
        {
            var add = TempData["record"];
            var idchkspecimen = db.idrl.Where(x => x.specimen == specimen).FirstOrDefault();
            var spchkspecimen = db.specimens.Where(x => x.specimen == specimen).FirstOrDefault();

            if (add != null)
            {

                return RedirectToAction("specimen_index", "Forms", new { alert = "Record Added" });
            }




            if (specimen == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (spchkspecimen == null)
            {
                return HttpNotFound();
            }
            if (idchkspecimen != null)
            {
                TempData["alert"] = "Record Already Exist";

                return RedirectToAction("specimen_index", "Forms", new { alert = "Record Already Exist" });

            }


            TempData["specimen"] = specimen;

            return View();
        }

        [Authorize]
        [HttpPost, ActionName("molecular_section3_fill")]

        [ValidateAntiForgeryToken]
        public ActionResult molecular_section3_fill(idrl idrl, idrlvsection3 idrlsec3)
        {
            var specimenschk = TempData["specimen"];
            string specimens = specimenschk.ToString();
            var siteidchk = db.specimens.Where(x => x.specimen == specimens.ToString()).Select(x => x.site_id).Max();
            int siteid = siteidchk;

            string name = User.Identity.Name;
            if (ModelState.IsValid)
            {
                idrl.site_id = siteid;
                idrl.specimen = specimens;

                idrl.target1gcl = idrlsec3.v3target1gcl;
                idrl.target2gcl = idrlsec3.v3target2gcl;
                idrl.target1limitgcl = idrlsec3.v3target1limitgcl;
                idrl.target2limitgcl = idrlsec3.v3target2limitgcl;
                idrl.target1pre_absen = idrlsec3.v3target1pre_absen;
                idrl.target2pre_absen = idrlsec3.v3target2pre_absen;
                //idrl.flowm3day = idrlsec3.v3flowm3day;
                //idrl.passqaqc = idrlsec3.v3passqaqc;
                //idrl.waterquality = idrlsec3.v3waterquality;
                //idrl.watervalue = idrlsec3.v3watervalue;
                //idrl.collectmethod = idrlsec3.v3collectmethod;

                idrl.flowm3day = "NA";
                idrl.passqaqc = "NA";
                idrl.waterquality = "NA";
                idrl.watervalue = "NA";
                idrl.collectmethod = "BMFS";



                idrl.entry_date = DateTime.Now.ToString(("dd/MM/yyyy HH:mm:ss"));
                idrl.note = idrlsec3.v3note;

                idrl.user_name = name;
                db.idrl.Add(idrl);
                db.SaveChanges();
                TempData["record"] = "Record added";
                return Json(new { success = true, responseText = "Data Insert Successfully" }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { success = false, responseText = "Contact Developer Team" }, JsonRequestBehavior.AllowGet);
        }


        public class idrlvsection1
        {
            public string v1datesample { get; set; }
            public string v1timesample { get; set; }
            public string v1receivedby { get; set; }
            public string v1temloggerinclude { get; set; }
            public string v1tempcoleman { get; set; }
            public string v1samplereceipt { get; set; }
            public string v1laboratoryremark { get; set; }
            public string v1rnaextractiondate { get; set; }
            public string v1rnaremarkcheck { get; set; }

        }

        public class idrlvsection2
        {
            public string v2realtimepcrn1ddmmyy { get; set; }
            public string v2realtimepcrn1gen { get; set; }
            public string v2ctvaluep1 { get; set; }
            public string v2n1gneremark { get; set; }
            public string v2realtimepcrn2ddmmyy { get; set; }
            public string v2realtimepcrn2gen { get; set; }
            public string v2ctvaluep2 { get; set; }
            public string v2n2gneremark { get; set; }
            public string v2realtimepcrnEgen { get; set; }
            public string v2realtimepcrn3Egen { get; set; }
            public string v2ctvaluep3 { get; set; }
            public string v2n_e_gneremark { get; set; }
        }
        public class idrlvsection3
        {
            public string v3target1gcl { get; set; }
            public string v3target2gcl { get; set; }
            public string v3target1limitgcl { get; set; }
            public string v3target2limitgcl { get; set; }
            public string v3target1pre_absen { get; set; }
            public string v3target2pre_absen { get; set; }
            public string v3flowm3day { get; set; }
            public string v3passqaqc { get; set; }
            public string v3waterquality { get; set; }
            public string v3watervalue { get; set; }
            public string v3collectmethod { get; set; }
            public string v3note { get; set; }
        }
    }
}