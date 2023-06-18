using EM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EM.Controllers
{
    public class SkillController : Controller
    {
        EMDbContext db = new EMDbContext();
        // GET: Skill
        public ActionResult Index()
        {
            return View(db.Skills.Include(x => x.Employee).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Employees = db.Employees.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Skill model)
        {
            if (ModelState.IsValid)
            {
                db.Skills.Add(model);
                db.SaveChanges();
                return PartialView("_SuccessPartial");
            }
            ViewBag.Employees = db.Employees.ToList();
            return PartialView("_FailPartial");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Employees = db.Employees.ToList();
            return View(db.Skills.First(x => x.SkillId == id));
        }
        [HttpPost]
        public ActionResult Edit(Skill model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_SuccessPartial");
            }
            ViewBag.Employees = db.Employees.ToList();
            return PartialView("_FailPartial");
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var data = new Skill { SkillId = id };
            db.Entry(data).State = EntityState.Deleted;
            db.SaveChanges();
            return Json(new { id = id });
        }
    }
}