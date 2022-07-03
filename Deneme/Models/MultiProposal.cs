using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class MultiProposal
    {
        public ProfessorTbl Proftbl { get; set; }
        public ProjectTbl Projtbl { get; set; }
        public AssistantTbl Assisttbl { get; set; }
        public Professor_ProjectTbl Profprjtbl { get; set; }
    }
}