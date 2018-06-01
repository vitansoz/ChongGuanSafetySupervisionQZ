namespace ChongGuanSafetySupervisionQZ.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QZ_Areas
    {
        [Key]
        [StringLength(32)]
        public string AreaId { get; set; }

        [Required]
        [StringLength(32)]
        public string AreaName { get; set; }

        public int AreaLevel { get; set; }

        [Required]
        [StringLength(32)]
        public string AreaPid { get; set; }
    }
}
