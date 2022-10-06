using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reporter.Models.Generated
{
    [Table("profession")]
    public partial class Profession
    {
        public Profession()
        {
            Professionals = new HashSet<Professional>();
        }

        [Key]
        [Column("IdPFS", TypeName = "int(11)")]
        public int IdPfs { get; set; }
        [Column("NamePFS")]
        [StringLength(50)]
        public string? NamePfs { get; set; }
        [Column("DescriptionPFS")]
        [StringLength(255)]
        public string? DescriptionPfs { get; set; }
        [Column("PicturePFS")]
        public string? PicturePfs { get; set; }
        [Column("BannerPFS")]
        public string? BannerPfs { get; set; }

        [InverseProperty("IdPfs1Navigation")]
        public virtual ICollection<Professional> Professionals { get; set; }
    }
}
