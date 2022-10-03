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
    public partial class Project
    {
        public Project()
        {
            Notifications = new HashSet<Notification>();
            Tags = new HashSet<Tag>();
        }

        [Key]
        [Column("IdPJ")]
        [StringLength(21)]
        public string IdPj { get; set; } = null!;
        [Column("TitlePJ")]
        [StringLength(50)]
        public string? TitlePj { get; set; }
        [Column("DescriptionPJ")]
        [StringLength(50)]
        public string? DescriptionPj { get; set; }
        [Column("PicturePJ")]
        public string? PicturePj { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column("TotalPricePJ")]
        public float? TotalPricePj { get; set; }
        [Column("IsPaidPJ")]
        public bool? IsPaidPj { get; set; }
        public bool? IsActive { get; set; }
        public bool? Completed { get; set; }
        [Column("TagDurationPJ", TypeName = "int(11)")]
        public int? TagDurationPj { get; set; }
        [StringLength(21)]
        public string? IdP1 { get; set; }
        [StringLength(21)]
        public string? IdC1 { get; set; }

        [ForeignKey("IdC1")]
        [InverseProperty("Projects")]
        public virtual Client? IdC1Navigation { get; set; }
        [ForeignKey("IdP1")]
        [InverseProperty("Projects")]
        public virtual Professional? IdP1Navigation { get; set; }
        [InverseProperty("IdPj2Navigation")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("IdPj1Navigation")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
