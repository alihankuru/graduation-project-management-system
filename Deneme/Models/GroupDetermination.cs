using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Group.Models
{
    public class GroupDetermination
    {
        [DisplayName("Group Name:")]
        public string GroupName { get; set; }
        [DisplayName("First Student Name:")]
        public string Name1 { get; set; }
        [DisplayName("Second Student Name:")]
        public string Name2 { get; set; }
        [DisplayName("First Student Id:")]
        public string StudentNo1 { get; set; }
        [DisplayName("Second Student Id:")]
        public string StudentNo2 { get; set; }

    }
}