//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Deneme.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FeedbackTbl
    {
        public int FeedbackID { get; set; }
        public int AssignProject { get; set; }
        public int AssignStudent { get; set; }
        public string FeedBackSubject { get; set; }
        public string FeedBackMessage { get; set; }
        public System.DateTime ReceivedDate { get; set; }
    
        public virtual ProjectTbl ProjectTbl { get; set; }
        public virtual StudentTbl StudentTbl { get; set; }
    }
}
