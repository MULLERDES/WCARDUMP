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
    
    public partial class CarObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarObject()
        {
            this.CarDumps = new HashSet<CarDump>();
        }
    
        public int Id { get; set; }
        public int AutoModelID { get; set; }
        public int Year { get; set; }
        public int TransmissionID { get; set; }
        public Nullable<decimal> Odometr { get; set; }
        public Nullable<decimal> Odometr2 { get; set; }
        public Nullable<decimal> EngineCapacity { get; set; }
        public Nullable<int> FuelTypeID { get; set; }
        public Nullable<int> MetricID { get; set; }
        public string Description { get; set; }
    
        public virtual AutoModel AutoModel { get; set; }
        public virtual FuelType FuelType { get; set; }
        public virtual MetricType MetricType { get; set; }
        public virtual TransmissionType TransmissionType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarDump> CarDumps { get; set; }
    }
}
