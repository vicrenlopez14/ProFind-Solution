using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("project")]
    [Index("IdC1", Name = "FK_Project_Client")]
    [Index("IdP1", Name = "FK_Project_Professional")]
    [Index("TimeRequiredTr1", Name = "FK_TimeRequired_Project")]
    public partial class Project
    {
        [Key]
        [Column("IdPJ")]
        [StringLength(21)]
        public string IdPj { get; set; } = null!;
        [Column("TitlePJ")]
        [StringLength(50)]
        public string? TitlePj { get; set; }
        [Column("DescriptionPJ")]
        [StringLength(150)]
        public string? DescriptionPj { get; set; }
        [Column("PicturePJ")]
        public string? PicturePj { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column("TotalPricePJ")]
        public float? TotalPricePj { get; set; }
        public bool? IsActive { get; set; }
        public bool? Completed { get; set; }
        [StringLength(21)]
        public string? IdP1 { get; set; }
        [StringLength(21)]
        public string? IdC1 { get; set; }
        [Column("TimeRequiredTR1", TypeName = "int(11)")]
        public int? TimeRequiredTr1 { get; set; }

        [ForeignKey("IdC1")]
        [InverseProperty("Projects")]
        public virtual Client? IdC1Navigation { get; set; }
        [ForeignKey("IdP1")]
        [InverseProperty("Projects")]
        public virtual Professional? IdP1Navigation { get; set; }
        [ForeignKey("TimeRequiredTr1")]
        [InverseProperty("Projects")]
        public virtual Timerequired? TimeRequiredTr1Navigation { get; set; }
    }
}
