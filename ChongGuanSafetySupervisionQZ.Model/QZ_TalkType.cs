using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace ChongGuanSafetySupervisionQZ.Model
{
    public class QZ_TalkType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TalkTypeId { get; set; }

        [Required]
        [StringLength(128)]
        public string TalkTypeName { get; set; }
    }
}
