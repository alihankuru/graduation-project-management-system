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
    public class ProfessorController : Controller
    {
        GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

        [Authorize(Roles = "Professor")]
        public ActionResult ProfessorProfile()
        {

            GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

            List<ProfessorTbl> Profinfo = db.ProfessorTbl.ToList();
            List<ProjectTbl> Projinfo = db.ProjectTbl.ToList();
            List<AssistantTbl> Assistinfo = db.AssistantTbl.ToList();
            List<Professor_ProjectTbl> Professorprojectinfo = db.Professor_ProjectTbl.ToList();

            var multiproposal = from p in Profinfo
                                    //join proprj in Professorprojectinfo on p.AssignProject equals proprj.ProjectID into table1
                                join proprj in Professorprojectinfo on p.ProfessorID equals proprj.ProfessorID into table4
                                from proprj in table4.DefaultIfEmpty()
                                join prj in Projinfo on proprj.ProjectID equals prj.ProjectID

                                join a in Assistinfo on p.AssignAssistan equals a.AssistantID into table2
                                from a in table2.DefaultIfEmpty()


                                select new MultiProposal { Proftbl = p, Projtbl = prj, Assisttbl = a, Profprjtbl = proprj };







            return View(multiproposal);
        }



        [Authorize(Roles = "Professor")]
        [HttpGet]
        public ActionResult ProposalForm()
        {

            return View();
        }

        [Authorize(Roles = "Professor")]
        [HttpPost]
        public ActionResult ProposalForm(ProposalModel membervalues)
        {

            //Use Namespace called :  System.IO
            string FileName = Path.GetFileNameWithoutExtension(membervalues.ImageFile.FileName);

            //To Get File Extension
            string FileExtension = Path.GetExtension(membervalues.ImageFile.FileName);

            //Add Current Date To Attached File Name
            FileName = FileName.Trim() + FileExtension;

            //Get Upload path from Web.Config file AppSettings.
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

            //Its Create complete path to store in server.
            membervalues.ImagePath = UploadPath + FileName;

            //To copy and save file into server.
            membervalues.ImageFile.SaveAs(membervalues.ImagePath);


            try
            {
                GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

                ProjectTbl projectTbl = new ProjectTbl();

                projectTbl.ProjectName = membervalues.Name;

                projectTbl.ProjectDescription = membervalues.Details;

                projectTbl.ProjectStartDate = membervalues.ProjectRepDate;

                projectTbl.ProjectEndDate = membervalues.LastSubmissionDate;

                projectTbl.ProjectUploadFile =FileName;

                db.ProjectTbl.Add(projectTbl);

                db.SaveChanges();


            }

            catch (Exception)
            {
                throw;
            }

            ViewBag.SuccesMessage = "Registeration Succesful";

            return View();

        }




        [Authorize(Roles = "Professor")]
        [HttpGet]
        public ActionResult ConfirmOrder(int Group_ProjectID)
        {

            using (GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities())
            {

                var query = db.Group_ProjectTbl.SingleOrDefault(m => m.Group_ProjectID ==Group_ProjectID);
                return PartialView(query);

            }
        }

        [Authorize(Roles = "Professor")]
        [HttpPost]
        public ActionResult ConfirmOrder(ProjectTbl o, Group_ProjectTbl o1, ApproveClass o2)
        {


            Group_ProjectTbl inv = new Group_ProjectTbl()
            {

                Group_ProjectID=o1.Group_ProjectID,
                AssignGroupID = o1.AssignGroupID,
                AssignProjectID=o1.AssignProjectID,
                Status=1,

            };



            db.Entry(inv).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ApproveDenyButtonPage", "Professor");
        }



        [Authorize(Roles = "Professor")]
        [HttpGet]
        public ActionResult DenyOrder(int Group_ProjectID)
        {
            using (GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities())
            {

                var query = db.Group_ProjectTbl.SingleOrDefault(m => m.Group_ProjectID == Group_ProjectID);
                return PartialView(query);

            }
        }

        [Authorize(Roles = "Professor")]
        [HttpPost]
        public ActionResult DenyOrder(Group_ProjectTbl o1)
        {
            Group_ProjectTbl inv = new Group_ProjectTbl()
            {
                Group_ProjectID=o1.Group_ProjectID,
                AssignGroupID = o1.AssignGroupID,
                AssignProjectID=o1.AssignProjectID,
                Status=-1,
            };


            db.Entry(inv).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ApproveDenyButtonPage", "Professor");
        }




        [Authorize(Roles = "Professor")]
        [HttpGet]
        public ActionResult ApproveDenyButtonPage()
        {


            GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

            List<Group_ProjectTbl> GroupProjectInfo = db.Group_ProjectTbl.ToList();
            List<ProjectTbl> ProjInfo = db.ProjectTbl.ToList();
            List<GroupTbl> GroupInfo = db.GroupTbl.ToList();
            List<Student_GroupTbl> StudentGroupInfo = db.Student_GroupTbl.ToList();
            List<StudentTbl> StudentInfo = db.StudentTbl.ToList();
            List<Student_ProjectTbl> StudentProjectInfo = db.Student_ProjectTbl.ToList();



            var query = from g in GroupInfo
                        join grpprj in GroupProjectInfo on g.GroupID equals grpprj.AssignGroupID into table1
                        from grpprj in table1.DefaultIfEmpty()
                        join prj in ProjInfo on grpprj.AssignProjectID equals prj.ProjectID into table2
                        from prj in table2.DefaultIfEmpty()
                        join stdprj in StudentProjectInfo on prj.ProjectID equals stdprj.ProjectID into table3
                        from stdprj in table3.DefaultIfEmpty()
                        join s1 in StudentInfo on stdprj.StudentID equals s1.StudentID into table4
                        from s1 in table4.DefaultIfEmpty()
                        join stdgrp in StudentGroupInfo on s1.StudentID equals stdgrp.AssignStudentID into table5
                        from stdgrp in table5.DefaultIfEmpty()
                        join s2 in StudentInfo on stdgrp.AssignStudentID2 equals s2.StudentID into table6
                        from s2 in table6.DefaultIfEmpty()


                        select new ApproveClass { grptbl = g, prjtbl = prj, grpprjtbl = grpprj, studenttbl1 = s1, studenttbl2 = s2, stdgrptbl =stdgrp, stdprjtbl = stdprj, };



            return View(query);
        }



        [Authorize(Roles = "Professor")]
        public ActionResult ViewAppliance()
        {
            GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();

            List<GroupTbl> groupInfo = db.GroupTbl.ToList();
            List<ProjectTbl> projectInfo = db.ProjectTbl.ToList();
            List<Group_ProjectTbl> groupProjectInfo = db.Group_ProjectTbl.ToList();


            var viewappliance = from g in groupInfo
                                join grpprj in groupProjectInfo on g.GroupID equals grpprj.AssignGroupID into table4
                                from grpprj in table4.DefaultIfEmpty()
                                join prj in projectInfo on grpprj.AssignProjectID equals prj.ProjectID into table5
                                from prj in table5.DefaultIfEmpty()



                                select new GroupApplianceView { groupTbl = g, projectTbl = prj, group_projectTbl = grpprj };


            return View(viewappliance);
        }

        [Authorize(Roles = "Professor")]
        public ActionResult ProfSeeGroupReports()
        {
            GraduationProjectManagementDBEntities db = new GraduationProjectManagementDBEntities();
            List<GroupTbl> groupInfo = db.GroupTbl.ToList();
            var seereports = from g in groupInfo
                             select new StudentandGroup { Grptbl = g };




            return View(seereports);
        }

    }
}