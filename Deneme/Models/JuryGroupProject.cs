using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class JuryGroupProject
    {
        public JuriesTbl juriesTbls { get; set; }

        public GroupTbl groupTbls { get; set; }

        public ProjectTbl projectTbls { get; set; }

        public VoteTbl voteTbls { get; set; }

        public GroupRatingResultTbl groupRatingResultTbls { get; set; }

        public ProfessorTbl Proftbl { get; set; }

        public Jury_ProfessorTbl Jury_ProfessorTbl { get; set; }

        public Jury_GroupTbl Jury_GroupTbl { get; set; }
    }
}