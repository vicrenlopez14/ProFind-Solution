using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("projecttemplate")]
    [Index("IdP1", Name = "FK_ProjectTemplate_Professional")]
    public partial class Projecttemplate
    {
        public Projecttemplate()
        {
            Projectpaytemplates = new HashSet<Projectpaytemplate>();
            Tagtemplates = new HashSet<Tagtemplate>();
        }

        [Key]
        [Column("IdPT")]
        [StringLength(21)]
        public string IdPt { get; set; } = null!;
        [Column("TitlePT")]
        [StringLength(50)]
        public string? TitlePt { get; set; }
        [Column("DescriptionPT")]
        [StringLength(50)]
        public string? DescriptionPt { get; set; }
        [Column("PicturePT")]
        public byte[]? PicturePt { get; set; }
        [Column("TotalPricePT")]
        public float? TotalPricePt { get; set; }
        [Column("SaveTagsPT")]
        public bool? SaveTagsPt { get; set; }
        [Column("SaveProjectPaysPT")]
        public bool? SaveProjectPaysPt { get; set; }
        [StringLength(21)]
        public string? IdP1 { get; set; }

        [ForeignKey("IdP1")]
        [InverseProperty("Projecttemplates")]
        public virtual Professional? IdP1Navigation { get; set; }
        [InverseProperty("IdPt1Navigation")]
        public virtual ICollection<Projectpaytemplate> Projectpaytemplates { get; set; }
        [InverseProperty("IdPt1Navigation")]
        public virtual ICollection<Tagtemplate> Tagtemplates { get; set; }
    }
}
