using EM.Models;
using EM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EM.Controllers
{
    public class EmployeeController : Controller
    {
       
        EMDbContext db = new EMDbContext();
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult EmployeeList()
        {
            return PartialView("_EmployeePartial", db.Employees.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Employee = db.Employees.ToList();
            return View();
        }
        [HttpPost]
        public PartialViewResult Create(EmployeeViewModel data)
        {
            if (ModelState.IsValid)
            {
                var c = new Employee
                {
                    EmployeeName = data.EmployeeName,
                    EmployeeDate = data.EmployeeDate,
                    EmployeeCategory = data.EmployeeCategory,
                    EmployeeRate = data.EmployeeRate,
                    WorkFromHome = data.WorkFromHome
                };
                if (data.Picture.ContentLength > 0)
                {
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Pictures"), fileName);
                    data.Picture.SaveAs(savePath);
                    c.Picture = fileName;
                }
                db.Employees.Add(c);
                db.SaveChanges();
                return PartialView("_SuccessPartial");
            }
            ViewBag.Employee = db.Employees.ToList();
            return PartialView("_FailPartial");
        }
        public ActionResult Edit(int id)
        {
            var data = db.Employees.FirstOrDefault(c => c.EmployeeId == id);
            if (data == null) return new HttpNotFoundResult();
            ViewBag.CurrentPicture = data.Picture;
            return View(new EmployeeEditModel
            {
             EmployeeId = data.EmployeeId,
             EmployeeName = data.EmployeeName,
             EmployeeCategory = data.EmployeeCategory,
             EmployeeDate = data.EmployeeDate,
             EmployeeRate = data.EmployeeRate,
             WorkFromHome = data.WorkFromHome.HasValue ? data.WorkFromHome.Value : false
            });
        }
        [HttpPost]
        public PartialViewResult Edit(EmployeeEditModel data)
        {
            var c = db.Employees.FirstOrDefault(x => x.EmployeeId == data.EmployeeId);
            if (c == null) return PartialView("_FailPartial");
            if (ModelState.IsValid)
            {
                c.EmployeeName = data.EmployeeName;
                c.EmployeeDate = data.EmployeeDate;
                c.EmployeeCategory = data.EmployeeCategory;
                c.EmployeeRate = data.EmployeeRate;
                c.WorkFromHome = data?.WorkFromHome;

                if (data.Picture != null)
                {
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Pictures"), fileName);
                    data.Picture.SaveAs(savePath);
                    c.Picture = fileName;
                }

                db.SaveChanges();
                return PartialView("_SuccessPartial");
            }
            return PartialView("_FailPartial");
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            if (db.Skills.Any(x => x.EmployeeId == id))
            {
                return Json(new { success = false, id = 0 });
            }
            else
            {
                var c = new Employee { EmployeeId = id };
                db.Entry(c).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Json(new { success = true, id = id });
            }
        }
        [Route("Custom/Master")]
        public ActionResult MasterDetailInsert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMaster(EmployeeViewModel data)
        {
            if (ModelState.IsValid)
            {
                Employee c = new Employee
                {
                    EmployeeName = data.EmployeeName,
                    WorkFromHome = data.WorkFromHome,
                    EmployeeDate = data.EmployeeDate,
                    EmployeeRate = data.EmployeeRate,
                    EmployeeCategory = data.EmployeeCategory
                };
                if (data.Picture.ContentLength > 0)
                {
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Pictures"), fileName);
                    data.Picture.SaveAs(savePath);
                    c.Picture = fileName;
                }
                db.Employees.Add(c);
                db.SaveChanges();
                return Json(c);
            }
            return Json(data);
        }
        [HttpPost]
        public ActionResult AddQualifications(int id, Skill[] data)
        {
            var c = db.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (c == null) return new HttpNotFoundResult();
            foreach (var q in data)
            {
                c.Skills.Add(q);
            }
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}