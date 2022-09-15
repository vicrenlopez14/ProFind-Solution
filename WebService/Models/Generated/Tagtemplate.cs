using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("tagtemplate")]
    [Index("IdPt1", Name = "FK_TagTemplate_ProjectTemplate")]
    public partial class Tagtemplate
    {
        [Key]
        [Column("IdTT")]
        [StringLength(21)]
        public string IdTt { get; set; } = null!;
        [Column("NameTT")]
        [StringLength(50)]
        public string? NameTt { get; set; }
        [Column("ColorTT")]
        [MaxLength(3)]
        public byte[]? ColorTt { get; set; }
        [Column("IdPT1")]
        [StringLength(21)]
        public string? IdPt1 { get; set; }

        [ForeignKey("IdPt1")]
        [InverseProperty("Tagtemplates")]
        public virtual Projecttemplate? IdPt1Navigation { get; set; }
    }
}
