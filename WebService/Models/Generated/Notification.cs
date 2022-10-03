using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("notification")]
    [Index("IdP1", Name = "FK_Notification_Professional")]
    [Index("IdPj2", Name = "FK_Notification_Project")]
    public partial class Notification
    {
        [Key]
        [StringLength(21)]
        public string IdN { get; set; } = null!;
        [StringLength(50)]
        public string? TitleN { get; set; }
        [StringLength(500)]
        public string? DescriptionN { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTimeIssuedN { get; set; }
        public string? PictureN { get; set; }
        [StringLength(21)]
        public string? IdP1 { get; set; }
        [Column("IdPJ2")]
        [StringLength(21)]
        public string? IdPj2 { get; set; }

        [ForeignKey("IdP1")]
        [InverseProperty("Notifications")]
        public virtual Professional? IdP1Navigation { get; set; }
        [ForeignKey("IdPj2")]
        [InverseProperty("Notifications")]
        public virtual Project? IdPj2Navigation { get; set; }
    }
}
