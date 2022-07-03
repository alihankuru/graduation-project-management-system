using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proposal.Models
{
    public class ProposalModel
    {
        //To change label title value
        [DisplayName("Project Name")]
        public string Name { get; set; }
        public string Details { get; set; }
        [DisplayName("ProjectStartDate")]
        public DateTime ProjectRepDate { get; set; }
        [DisplayName("ProjectEndDate")]
        public DateTime LastSubmissionDate { get; set; }
        //To change label title value
        [DisplayName("Upload File")]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}