using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class ApproveClass
    {

        public ProjectTbl prjtbl { get; set; }
        public Group_ProjectTbl grpprjtbl { get; set; }
        public GroupTbl grptbl { get; set; }
        public StudentTbl studenttbl1 { get; set; }
        public StudentTbl studenttbl2 { get; set; }
        public Student_GroupTbl stdgrptbl { get; set; }
        public Student_ProjectTbl stdprjtbl { get; set; }


    }





}