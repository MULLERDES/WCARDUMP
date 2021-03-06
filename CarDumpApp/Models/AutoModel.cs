//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarDumpApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AutoModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AutoModel()
        {
            this.CarDumps = new HashSet<CarDump>();
            this.CarDumpSets = new HashSet<CarDumpSet>();
            this.CarObjects = new HashSet<CarObject>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int AutoBrandID { get; set; }
    
        public virtual AutoBrand AutoBrand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarDump> CarDumps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarDumpSet> CarDumpSets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarObject> CarObjects { get; set; }
    }
}
