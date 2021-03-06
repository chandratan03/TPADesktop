//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace program_mvm
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.DetailTransactions = new HashSet<DetailTransaction>();
            this.ProductHistories = new HashSet<ProductHistory>();
            this.Stocks = new HashSet<Stock>();
        }
    
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> MgfDate { get; set; }
        public Nullable<System.DateTime> ExpDate { get; set; }
        public string MeasuringUnit { get; set; }
        public Nullable<byte> IsActive { get; set; }
        public string PromoID { get; set; }
        public Nullable<int> RestockPoint { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailTransaction> DetailTransactions { get; set; }
        public virtual Promo Promo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductHistory> ProductHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
