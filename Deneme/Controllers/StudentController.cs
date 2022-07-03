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
    public class StudentController : Controller
    {
        GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

        [Authorize(Roles = "Student")]
        public ActionResult StudentProfile()
        {

            GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

            List<ProfessorTbl> Profinfo = db.ProfessorTbl.ToList();
            List<ProjectTbl> Projinfo = db.ProjectTbl.ToList();
            List<AssistantTbl> Assistinfo = db.AssistantTbl.ToList();
            List<StudentTbl> Studentinfo = db.StudentTbl.ToList();
            List<Student_ProjectTbl> Studentprojectinfo = db.Student_ProjectTbl.ToList();
            List<Professor_ProjectTbl> Professorprojectinfo = db.Professor_ProjectTbl.ToList();





            var StudentProfile = from s in Studentinfo
                                 join stuprj in Studentprojectinfo on s.StudentID equals stuprj.StudentID into table4
                                 from stuprj in table4.DefaultIfEmpty()
                                 join prj in Projinfo on stuprj.ProjectID equals prj.ProjectID into table5
                                 from prj in table5.DefaultIfEmpty()



                                 select new StudentProjectSee { /*Proftbl = p,*/ Projtbl = prj,/* Assisttbl = a ,*/ Stutbl = s,/* Profprjtbl=proprj ,*/ Stuprjtbl = stuprj };
            // select new StudentProjectSee { Proftbl = p, Projtbl = prj, Assisttbl = a, Profprjtbl = proprj };
            return View(StudentProfile);

        }










        [Authorize(Roles = "Student")]
        public ActionResult Deneme()
        {

            GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

            List<ProfessorTbl> Profinfo = db.ProfessorTbl.ToList();
            List<ProjectTbl> Projinfo = db.ProjectTbl.ToList();
            List<AssistantTbl> Assistinfo = db.AssistantTbl.ToList();
            List<Professor_ProjectTbl> Professorprojectinfo = db.Professor_ProjectTbl.ToList();

            var multiproposal = from p in Profinfo

                                join proprj in Professorprojectinfo on p.ProfessorID equals proprj.ProfessorID into table4
                                from proprj in table4.DefaultIfEmpty()
                                join prj in Projinfo on proprj.ProjectID equals prj.ProjectID

                                join a in Assistinfo on p.AssignAssistan equals a.AssistantID into table2
                                from a in table2.DefaultIfEmpty()


                                select new MultiProposal { Proftbl = p, Projtbl = prj, Assisttbl = a, Profprjtbl = proprj };

            return View(multiproposal);

        }





        [Authorize(Roles = "Student")]
        public ActionResult SeeApply()
        {
            GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

            List<ProfessorTbl> Profinfo = db.ProfessorTbl.ToList();
            List<ProjectTbl> Projinfo = db.ProjectTbl.ToList();
            List<AssistantTbl> Assistinfo = db.AssistantTbl.ToList();
            List<StudentTbl> Studentinfo = db.StudentTbl.ToList();
            List<Student_ProjectTbl> Studentprojectinfo = db.Student_ProjectTbl.ToList();
            List<Professor_ProjectTbl> Professorprojectinfo = db.Professor_ProjectTbl.ToList();

            var seeapply = from s in Studentinfo
                           join stuprj in Studentprojectinfo on s.StudentID equals stuprj.StudentID into table4
                           from stuprj in table4.DefaultIfEmpty()
                           join prj in Projinfo on stuprj.ProjectID equals prj.ProjectID into table5
                           from prj in table5.DefaultIfEmpty()



                           select new StudentProjectSee { Projtbl = prj, Stutbl = s, Stuprjtbl = stuprj };



            return View(seeapply);
        }





        [Authorize(Roles = "Student")]
        [HttpGet]
        public ActionResult Proposalconfirm(int ProjectID, StudentTbl o)
        {
            GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();



            List<Student_ProjectTbl> list = db.Student_ProjectTbl.ToList();

            ViewBag.Student_ProjectTbl = new SelectList(list, "StudentID", "ProjectID");
            return PartialView();
        }
        [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult Proposalconfirm(Applyproposal applyproposal)
        {

            try
            {
                GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

                Student_ProjectTbl student_ProjectTbl = new Student_ProjectTbl();

                student_ProjectTbl.StudentID = applyproposal.StudentID;

                student_ProjectTbl.ProjectID = applyproposal.ProjectID;

                db.Student_ProjectTbl.Add(student_ProjectTbl);

                db.SaveChanges();

                return RedirectToAction("SeeApply", "Student");

            }

            catch (Exception)
            {
                throw;
            }

        }




        [HttpGet]
        public ActionResult GroupUploadReport(Nullable<int> GroupID)
        {
            using (GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities())
            {

                var query = db.GroupTbl.SingleOrDefault(m => m.GroupID == GroupID);
                return PartialView(query);

            }
        }
        [HttpPost]
        public ActionResult GroupUploadReport(GroupTbl o, GroupReport o1)
        {


            string FileName = Path.GetFileNameWithoutExtension(o1.ImageFile1.FileName);


            string FileExtension = Path.GetExtension(o1.ImageFile1.FileName);


            FileName = FileName.Trim() + FileExtension;


            string UploadPath = ConfigurationManager.AppSettings["UserImagePath1"].ToString();


            o1.ImagePath1 = UploadPath + FileName;


            o1.ImageFile1.SaveAs(o1.ImagePath1);



            GroupTbl inv = new GroupTbl()
            {
                GroupID = o.GroupID,
                GroupName = o.GroupName,
                UploadReport = FileName
            };


            db.Entry(inv).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("StudentGroup", "Student");
        }





        [Authorize(Roles = "Student")]
        public ActionResult StudentGroup()
        {
            GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

            List<GroupTbl> GroupInfo = db.GroupTbl.ToList();
            List<StudentTbl> StudentInfo = db.StudentTbl.ToList();
            List<Student_GroupTbl> StudentGroupInfo = db.Student_GroupTbl.ToList();



            var Studentgroup =


                from g in GroupInfo
                join stdgrp in StudentGroupInfo on g.GroupID equals stdgrp.AssignGroupID into table4
                from stdgrp in table4.DefaultIfEmpty()
                join s in StudentInfo on stdgrp.AssignStudentID equals s.StudentID into table5
                join s in StudentInfo on stdgrp.AssignStudentID2 equals s.StudentID into table6
                from s in table5.DefaultIfEmpty()
                from s2 in table6.DefaultIfEmpty()







                select new StudentandGroup { Grptbl = g, Stdtbl = s, Stdgrptbl = stdgrp, Stdtbl2 = s2 };






            return View(Studentgroup);
        }





    }
}