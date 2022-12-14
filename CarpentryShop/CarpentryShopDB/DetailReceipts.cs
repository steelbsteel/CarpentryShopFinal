//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarpentryShop.CarpentryShopDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetailReceipts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DetailReceipts()
        {
            this.MetalDetails = new HashSet<MetalDetails>();
            this.WoodDetails = new HashSet<WoodDetails>();
        }
    
        public int idDetailReceipt { get; set; }
        public Nullable<int> idMaterial { get; set; }
        public Nullable<int> idTool { get; set; }
        public Nullable<int> idMachine { get; set; }
    
        public virtual Machines Machines { get; set; }
        public virtual Materials Materials { get; set; }
        public virtual Tools Tools { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MetalDetails> MetalDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WoodDetails> WoodDetails { get; set; }
    }
}
