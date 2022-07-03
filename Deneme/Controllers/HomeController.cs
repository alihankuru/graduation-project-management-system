using Deneme.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Group.Models;

using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium;
using Proposal.Models;

namespace Deneme.Controllers
{
    public class HomeController : Controller
    {
        GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

       
        [Authorize(Roles = "Student,Professor")]
        [HttpGet]
        public ActionResult ProjectGroupDetermination()
        {
            GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

            List<StudentTbl> list = db.StudentTbl.ToList();

            ViewBag.StudentTbls = new SelectList(list, "StudentID", "StudentNumber");

            return View();
        }
        [Authorize(Roles = "Student,Professor")]
        [HttpPost]
        public ActionResult ProjectGroupDetermination(DataClass dataClass)
        {
            try
            {

                GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

                GroupTbl groupTbl = new GroupTbl();

                groupTbl.GroupName = dataClass.GroupName;

                Student_GroupTbl student_GroupTbl = new Student_GroupTbl();

                student_GroupTbl.AssignStudentID = dataClass.StudentID;

                student_GroupTbl.AssignStudentID2 = dataClass.StudentID2;

                db.GroupTbl.Add(groupTbl);

                db.Student_GroupTbl.Add(student_GroupTbl);

                db.SaveChanges();
                ViewBag.SuccesMessage = "Registeration Succesful";
                return RedirectToAction("Index");

            }

            catch (Exception)
            {
                throw;
            }


            

            //return View();
        }

       
       

      


    }
}