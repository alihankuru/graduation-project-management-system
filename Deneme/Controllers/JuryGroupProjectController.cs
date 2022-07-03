using Deneme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deneme.Controllers
{
    public class JuryGroupProjectController : Controller
    {
        // GET: JuryGroupProjectController


        private GraduationProjectManagementDBEntities objRatingDbEntities;

        public JuryGroupProjectController()
        {

            objRatingDbEntities = new GraduationProjectManagementDBEntities();

        }

        // GET: JuryGroupProject
        [Authorize(Roles = "Professor")]
        public ActionResult Index()
        {

            List<JuriesTbl> juriesTbls = objRatingDbEntities.JuriesTbl.ToList();
            List<GroupTbl> groupTbls = objRatingDbEntities.GroupTbl.ToList();
            List<ProjectTbl> projectTbls = objRatingDbEntities.ProjectTbl.ToList();
            List<VoteTbl> voteTbls = objRatingDbEntities.VoteTbl.ToList();
            List<GroupRatingResultTbl> groupRatingResultTbls = objRatingDbEntities.GroupRatingResultTbl.ToList();

            List<ProfessorTbl> professorTbls = objRatingDbEntities.ProfessorTbl.ToList();
            List<Jury_ProfessorTbl> jury_ProfessorTbls = objRatingDbEntities.Jury_ProfessorTbl.ToList();
            List<Jury_GroupTbl> jury_GroupTbls = objRatingDbEntities.Jury_GroupTbl.ToList();

            var multipletable =



                                from pt in professorTbls
                                join jp in jury_ProfessorTbls on pt.ProfessorID equals jp.ProfessorID into table4
                                from jp in table4.DefaultIfEmpty()
                                join j in juriesTbls on jp.JuryID equals j.JuryID into table5
                                from j in table5.DefaultIfEmpty()
                                join jg in jury_GroupTbls on j.JuryID equals jg.JuryID into table6
                                from jg in table6.DefaultIfEmpty()
                                join g in groupTbls on jg.GroupID equals g.GroupID into table7
                                from g in table7.DefaultIfEmpty()
                                join p in projectTbls on j.AssignProjectID equals p.ProjectID into table2
                                from p in table2.DefaultIfEmpty()
                                join va in groupRatingResultTbls on j.AssignVoteID equals va.Id into table3
                                from va in table3.DefaultIfEmpty()




                                select new JuryGroupProject { juriesTbls = j, groupTbls = g, projectTbls = p, groupRatingResultTbls = va, Proftbl = pt, Jury_ProfessorTbl = jp, Jury_GroupTbl = jg };

            return View(multipletable);
        }

    }
}