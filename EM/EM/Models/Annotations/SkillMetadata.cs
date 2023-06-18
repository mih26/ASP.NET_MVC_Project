using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EM.Models.Annotations
{
    [MetadataType(typeof(SkillMetadata))]
    public partial class Skill
    {
    }
    public partial class SkillMetadata
    {
        public int SkillId { get; set; }
        [Required, StringLength(50)]
        public string Degree { get; set; }
        [Required]
        public int PassingYear { get; set; }
        [Required, StringLength(50)]
        public string Institute { get; set; }
        [Required, StringLength(20)]
        public string Result { get; set; }
        [Required]
        public int EmployeeId { get; set; }


    }
}