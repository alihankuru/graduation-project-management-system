using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class QuestionViewModel
    {

        public int QuestionID { get; set; }

        public string QuestionSubject { get; set; }

        public string QuestionDescription { get; set; }


        public int GroupID { get; set; }

    }
}