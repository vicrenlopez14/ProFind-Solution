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
    [Index("IdPs1", Name = "FK_Project_ProjectStatus")]
    public partial class Project
    {
        public Project()
        {
            Activities = new HashSet<Activity>();
            Notifications = new HashSet<Notification>();
            Projectpays = new HashSet<Projectpay>();
            Supporttickets = new HashSet<Supportticket>();
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
        public byte[]? PicturePj { get; set; }
        [Column("TotalPricePJ")]
        public float? TotalPricePj { get; set; }
        [Column("IdPS1")]
        [StringLength(21)]
        public string? IdPs1 { get; set; }
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
        [ForeignKey("IdPs1")]
        [InverseProperty("Projects")]
        public virtual Projectstatus? IdPs1Navigation { get; set; }
        [InverseProperty("IdPj1Navigation")]
        public virtual ICollection<Activity> Activities { get; set; }
        [InverseProperty("IdPj2Navigation")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("IdPj3Navigation")]
        public virtual ICollection<Projectpay> Projectpays { get; set; }
        [InverseProperty("IdPj4Navigation")]
        public virtual ICollection<Supportticket> Supporttickets { get; set; }
        [InverseProperty("IdPjNavigation")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
