namespace ChongGuanSafetySupervisionQZ.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QZ_User
    {
        [Key]
        [StringLength(32)]
        public string UserId { get; set; }

        [Required]
        [StringLength(32)]
        public string UserCode { get; set; }

        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        public string LoginName { get; set; }

        [Required]
        [StringLength(32)]
        public string LoginPwd { get; set; }

        [Required]
        [StringLength(32)]
        public string UserLawCard { get; set; }

        [Required]
        [StringLength(32)]
        public string AreaCode { get; set; }

        [Required]
        [StringLength(20)]
        public string UserCard { get; set; }

        [Required]
        [StringLength(20)]
        public string UserPhone { get; set; }

        [Required]
        [StringLength(4)]
        public string UserSex { get; set; }

        [Required]
        [StringLength(4)]
        public string UserAge { get; set; }

        public int IsForbidden { get; set; }

        [Required]
        [StringLength(128)]
        public string UserPhotoFilePath { get; set; }

        [Required]
        [StringLength(128)]
        public string UserFingerImageFilePath { get; set; }

        [Required]
        [StringLength(50)]
        public string UserEmail { get; set; }

        public int IsDeleteId { get; set; }

        [Required]
        [StringLength(20)]
        public string CreateTime { get; set; }

        [Required]
        [StringLength(20)]
        public string ModifyTime { get; set; }
    }
}
