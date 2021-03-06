//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VacationTrack
{
    using System;
    using System.Collections.Generic;
    
    public partial class VacationRequest
    {
        public int VacationID { get; set; }
        public Nullable<int> PersonID { get; set; }
        public int CategoryID { get; set; }
        public Nullable<int> DocumentID { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string AbsenceDetails { get; set; }
        public Nullable<decimal> AmountOfTime { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Person Person { get; set; }
    }
}
