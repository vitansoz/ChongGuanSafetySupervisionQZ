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

        [StringLength(32)]
        public string PartyNumber { get; set; }

        [StringLength(32)]
        public string PartyDetentionDate { get; set; }

        [StringLength(64)]
        public string PartyPrison { get; set; }

        [StringLength(64)]
        public string PartyMonitoringArea { get; set; }

        [StringLength(64)]
        public string PartyMonitoringRoom { get; set; }

        [StringLength(128)]
        public string PartyDetentionReason { get; set; }

        [Required]
        [StringLength(32)]
        public string PartyName { get; set; }

        [StringLength(2)]
        public string PartySex { get; set; }

        [StringLength(20)]
        public string PartyBirth { get; set; }

        [StringLength(20)]
        public string PartyNational { get; set; }

        [StringLength(32)]
        public string PartyCard { get; set; }

        [StringLength(128)]
        public string PartyAddress
        {
            get;
            set;
        }

        [StringLength(128)]
        public string PartyUnit { get; set; }

        [StringLength(20)]
        public string PartyPhone { get; set; }

        [StringLength(128)]
        public string PartyPosition { get; set; }

        [StringLength(128)]
        public string PartyCardImageFilePath { get; set; }

        [StringLength(128)]
        public string PartyFingerImageFilePath { get; set; }

        [StringLength(128)]
        public string PartyCardIssusOffice { get; set; }

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
