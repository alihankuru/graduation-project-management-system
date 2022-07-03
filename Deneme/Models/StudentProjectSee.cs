using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class StudentProjectSee
    {
        public ProfessorTbl Proftbl { get; set; }
        public ProjectTbl Projtbl { get; set; }
        public AssistantTbl Assisttbl { get; set; }
        public StudentTbl Stutbl { get; set; }
        public Student_ProjectTbl Stuprjtbl { get; set; }
        public Professor_ProjectTbl Profprjtbl { get; set; }
    }
}