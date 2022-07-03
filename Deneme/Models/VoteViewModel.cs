using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class VoteViewModel
    {

        public IEnumerable<QuestionTbl> questionTbls { get; set; }

        public int VoteID { get; set; }

        public int QuestionID { get; set; }

        public int? AssignGroup { get; set; }

        public string VoteDescription { get; set; }
        public string VoteDescription2 { get; set; }
        public string VoteDescription3 { get; set; }
        public string VoteDescription4 { get; set; }
        public string VoteDescription5 { get; set; }
        public string VoteDescription6 { get; set; }
        public string VoteDescription7 { get; set; }

        public string VoteDescription8 { get; set; }

        public int? Rating { get; set; }
        public int? Rating2 { get; set; }
        public int? Rating3 { get; set; }

        public int? Rating4 { get; set; }
        public int? Rating5 { get; set; }
        public int? Rating6 { get; set; }
        public int? Rating7 { get; set; }
        public int? Rating8 { get; set; }



        public DateTime? CommentedOn { get; set; }
    }
}