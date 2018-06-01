namespace ChongGuanSafetySupervisionQZ.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QZ_Party
    {
        [Key]
        [StringLength(32)]
        public string PartyId { get; set; }

        [Required]
        [StringLength(32)]
        public string PartyName { get; set; }

        [Required]
        [StringLength(2)]
        public string PartySex { get; set; }

        [Required]
        [StringLength(20)]
        public string PartyBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string PartyNational { get; set; }

        [Required]
        [StringLength(32)]
        public string PartyCard { get; set; }

        [Required]
        [StringLength(128)]
        public string PartyAddress { get; set; }

        [Required]
        [StringLength(128)]
        public string PartyUnit { get; set; }

        [Required]
        [StringLength(20)]
        public string PartyPhone { get; set; }

        [Required]
        [StringLength(128)]
        public string PartyPosition { get; set; }

        [Required]
        [StringLength(128)]
        public string PartyCardImageFilePath { get; set; }

        [Required]
        [StringLength(128)]
        public string PartyFingerImageFilePath { get; set; }

        [Required]
        [StringLength(128)]
        public string PartyCardIssusOffice { get; set; }

        [Required]
        [StringLength(20)]
        public string PartyCardLimitDate { get; set; }

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
