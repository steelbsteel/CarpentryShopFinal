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
    
    public partial class InventoryCarpenter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InventoryCarpenter()
        {
            this.InventoryComponents = new HashSet<InventoryComponents>();
            this.InventoryMaterials = new HashSet<InventoryMaterials>();
            this.InventoryMetalDetails = new HashSet<InventoryMetalDetails>();
            this.InventoryTools = new HashSet<InventoryTools>();
            this.InventoryWoodDetails = new HashSet<InventoryWoodDetails>();
        }
    
        public int idInventory { get; set; }
        public int idCarpenter { get; set; }
    
        public virtual Carpenter Carpenter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryComponents> InventoryComponents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryMaterials> InventoryMaterials { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryMetalDetails> InventoryMetalDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryTools> InventoryTools { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryWoodDetails> InventoryWoodDetails { get; set; }
    }
}
