using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("projectpaytemplate")]
    [Index("IdPt1", Name = "FK_ProjectPayTemplate_ProjectTemplate")]
    public partial class Projectpaytemplate
    {
        [Key]
        [Column("IdPPT")]
        [StringLength(21)]
        public string IdPpt { get; set; } = null!;
        [Column("TitlePPT")]
        [StringLength(50)]
        public string? TitlePpt { get; set; }
        [Column("DescriptionPPT")]
        [StringLength(50)]
        public string? DescriptionPpt { get; set; }
        [Column("PicturePPT")]
        public byte[]? PicturePpt { get; set; }
        [Column("TotalPricePPT")]
        public float? TotalPricePpt { get; set; }
        [Column("IdPT1")]
        [StringLength(21)]
        public string? IdPt1 { get; set; }

        [ForeignKey("IdPt1")]
        [InverseProperty("Projectpaytemplates")]
        public virtual Projecttemplate? IdPt1Navigation { get; set; }
    }
}
