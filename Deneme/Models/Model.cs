using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class Model
    {
        public string Instructorname { get; set; }
        public string Projectname { get; set; }
        public string Projectdetails { get; set; }
        public string Precondition { get; set; }
        public string Presentdate { get; set; }
        public string Submissiondate { get; set; }
        public string Adviserinfo { get; set; }
        public string Link { get; set; }
    }

    public class Proposal
    {
        public int TotalProposals { get; set; }
        public string Proposal1 { get; set; }
        public string Proposal2 { get; set; }
        public string Proposal3 { get; set; }
        public string Proposal4 { get; set; }
        public string Proposal5 { get; set; }
        public string Proposal6 { get; set; }
        public string Proposal7 { get; set; }
    }
}