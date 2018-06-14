namespace ChongGuanSafetySupervisionQZ.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QZ_Event
    {
        [Key]
        [StringLength(32)]
        public string EventId { get; set; }

        [Required]
        [StringLength(32)]
        public string EventCode { get; set; }

        [Required]
        [StringLength(128)]
        public string EventName { get; set; }

        [StringLength(128)]
        public string EventAddress { get; set; }

        [StringLength(500)]
        public string EventDesc { get; set; }

        public int EventStatus { get; set; }

        [StringLength(20)]
        public string EventTime { get; set; }

        [Required]
        [StringLength(32)]
        public string CreateUserId { get; set; }

        [Required]
        [StringLength(32)]
        public string CreateDepartmentId { get; set; }

        public int IsDeleteId { get; set; }

        [Required]
        [StringLength(20)]
        public string CreateTime { get; set; }

        [Required]
        [StringLength(20)]
        public string ModifyTime { get; set; }
    }
}
