using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("activity")]
    [Index("IdPj1", Name = "FK_Activity_Project")]
    [Index("IdT1", Name = "FK_Activity_Tag")]
    public partial class Activity
    {
        public Activity()
        {
            Activitycomments = new HashSet<Activitycomment>();
            Supporttickets = new HashSet<Supportticket>();
            }

        [Key]
        [StringLength(21)]
        public string IdA { get; set; } = null!;
        [StringLength(50)]
        public string? TitleA { get; set; }
        [StringLength(500)]
        public string? DescriptionA { get; set; }
        public DateOnly? ExpectedBeginA { get; set; }
        public DateOnly? ExpectedEndA { get; set; }
        public byte[]? PictureA { get; set; }
        [Column("IdPJ1")]
        [StringLength(21)]
        public string? IdPj1 { get; set; }
        [StringLength(21)]
        public string? IdT1 { get; set; }

        [ForeignKey("IdPj1")]
        [InverseProperty("Activities")]
        public virtual Project? IdPj1Navigation { get; set; }
        [ForeignKey("IdT1")]
        [InverseProperty("Activities")]
        public virtual Tag? IdT1Navigation { get; set; }
        [InverseProperty("IdA1Navigation")]
        public virtual ICollection<Activitycomment> Activitycomments { get; set; }
        [InverseProperty("IdAct1Navigation")]
        public virtual ICollection<Supportticket> Supporttickets { get; set; }
    }
}
