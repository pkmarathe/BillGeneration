//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillGeneration.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class FontMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FontMaster()
        {
            this.CategoryRecieptMasters = new HashSet<CategoryRecieptMaster>();
            this.BillRecieptTables = new HashSet<BillRecieptTable>();
        }
    
        public int FontId { get; set; }
        public string FontStyle { get; set; }
        public string FontLink { get; set; }
        public string FontClass { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryRecieptMaster> CategoryRecieptMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillRecieptTable> BillRecieptTables { get; set; }
    }
}
