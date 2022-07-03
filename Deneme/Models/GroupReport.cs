using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class GroupReport
    {
        public GroupTbl groupTbl { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }

        [DisplayName("Upload File")]
        public string ImagePath1 { get; set; }
        public HttpPostedFileBase ImageFile1 { get; set; }
    }
}