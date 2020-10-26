//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class customersInLine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customersInLine()
        {
            this.ranges = new HashSet<ranx>();
            this.swaps = new HashSet<swap>();
        }
    
        public int TurnId { get; set; }
        public int custId { get; set; }
        public int activityTimeId { get; set; }
        public Nullable<System.TimeSpan> enterHour { get; set; }
        public System.DateTime estimatedHour { get; set; }
        public Nullable<System.TimeSpan> actualHour { get; set; }
        public Nullable<System.TimeSpan> exitHour { get; set; }
        public int preAlert { get; set; }
        public int statusTurn { get; set; }
        public string verificationCode { get; set; }
        public Nullable<int> numOfPushTimes { get; set; }
        public Nullable<bool> isActive { get; set; }
    
        public virtual activityTime activityTime { get; set; }
        public virtual customer customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ranx> ranges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<swap> swaps { get; set; }
    }
}
