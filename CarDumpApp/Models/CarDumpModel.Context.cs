﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CarDumpDatabaseEntities : DbContext
    {
        public CarDumpDatabaseEntities()
            : base("name=CarDumpDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AutoBrand> AutoBrands { get; set; }
        public virtual DbSet<AutoModel> AutoModels { get; set; }
        public virtual DbSet<CarDump> CarDumps { get; set; }
        public virtual DbSet<MemoryType> MemoryTypes { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<StoredFile> StoredFiles { get; set; }
        public virtual DbSet<CarDumpAccessLevel> CarDumpAccessLevels { get; set; }
    }
}
