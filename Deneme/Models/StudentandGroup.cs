using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class StudentandGroup
    {
        public GroupTbl Grptbl { get; set; }
        public StudentTbl Stdtbl { get; set; }
        public StudentTbl Stdtbl2 { get; set; }
        public Student_GroupTbl Stdgrptbl { get; set; }
    }
}