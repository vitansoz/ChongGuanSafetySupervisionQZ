namespace ChongGuanSafetySupervisionQZ.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QZ_Deparment
    {
        //[Key]
        //[StringLength(32)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DeparmentId { get; set; }

        [Required]
        [StringLength(32)]
        public string DeparmentCode { get; set; }

        [Required]
        [StringLength(32)]
        public string DeparmentName { get; set; }

        [StringLength(32)]
        public string DeparmentParentCode { get; set; }

        [Required]
        [StringLength(32)]
        public string AreaCode { get; set; }

        public int IsDeleteId { get; set; }

        [Required]
        [StringLength(20)]
        public string CreateTime { get; set; }

        [Required]
        [StringLength(20)]
        public string ModifyTime { get; set; }
    }
}
