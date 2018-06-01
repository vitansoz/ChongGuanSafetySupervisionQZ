namespace ChongGuanSafetySupervisionQZ.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QZ_Inquiry
    {
        [Key]
        [StringLength(32)]
        public string InquiryId { get; set; }

        [Required]
        [StringLength(32)]
        public string InquiryCode { get; set; }

        [Required]
        [StringLength(20)]
        public string InquiryDate { get; set; }

        [Required]
        [StringLength(128)]
        public string InquiryAddress { get; set; }

        [Required]
        [StringLength(32)]
        public string EventId { get; set; }

        [Required]
        [StringLength(32)]
        public string PartyId { get; set; }

        [Required]
        [StringLength(128)]
        public string Users { get; set; }

        [Required]
        [StringLength(128)]
        public string InquiryChatFilePath { get; set; }

        [Required]
        [StringLength(128)]
        public string InquiryAudioFilePath { get; set; }

        [Required]
        [StringLength(128)]
        public string InquiryVideo1FilePath { get; set; }

        [Required]
        [StringLength(128)]
        public string InquiryVideo2FilePath { get; set; }

        [Required]
        [StringLength(128)]
        public string InquiryVideo3FilePath { get; set; }

        [Required]
        [StringLength(128)]
        public string InquiryVideo4FilePath { get; set; }

        [Required]
        [StringLength(128)]
        public string InquiryLawBookFilePath { get; set; }

        [Required]
        [StringLength(20)]
        public string InquiryBeginTime { get; set; }

        [Required]
        [StringLength(20)]
        public string InquiryEndTime { get; set; }

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
