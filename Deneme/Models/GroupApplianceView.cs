using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class GroupApplianceView
    {
        public GroupTbl groupTbl { get; set; }

        public ProjectTbl projectTbl { get; set; }

        public Group_ProjectTbl group_projectTbl { get; set; }

    }
}