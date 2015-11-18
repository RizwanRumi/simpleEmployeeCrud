using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MvcEmployee.Models;

namespace MvcEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDBContext db = new EmployeeDBContext();
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            var employees = db.Employees.ToList();
            return View(employees);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Employee anEmployee)
        {
            string msg = "";
            int value = 1;
            HttpPostedFileBase imageFile = Request.Files["UploadedImage"];
            imageFile.SaveAs(HttpContext.Server.MapPath("~/ImageUpload/" + imageFile.FileName));
            anEmployee.ImagePath = imageFile.FileName;

            try
            {
                if (ModelState.IsValid)
                {
                    db.Employees.Add(anEmployee);
                    db.SaveChanges();

                    value = 1;
                    msg = "Successfully employee added";
                }
            }
            catch (Exception ex)
            {
                value = 2;
                msg = "there have an error.";
            }
            

            return Json(new{valid = value, message = msg,},JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public JsonResult Edit(Employee anEmployee)
        {
            string msg = "";
            int value = 1;
            HttpPostedFileBase imageFile = Request.Files["UploadedImage"];
            imageFile.SaveAs(HttpContext.Server.MapPath("~/ImageUpload/" + imageFile.FileName));
            anEmployee.ImagePath = imageFile.FileName;

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(anEmployee).State = EntityState.Modified;
                    db.SaveChanges();

                    value = 1;
                    msg = "Successfully updated";
                }
            }
            catch (Exception ex)
            {
                value = 2;
                msg = ex.Message;
            }
            return Json(new {valid = value,message = msg}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }

            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            //db.Employees.Remove(employee);
            //db.SaveChanges();

            //return RedirectToAction("Index");

            return View(employee);
        }

        [HttpPost]
        public JsonResult Delete(int? id, string name)
        {
            if (id == null)
            {
                return Json(new{valid = false, message = "There have no entry this id"});
            }

            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return Json(new { valid = false, message = "There have no employee" });
            }

            db.Employees.Remove(employee);
            db.SaveChanges();
            return Json(new { valid = true, message = "Successfully Deleted employee info." });
          
        }
    }
}
