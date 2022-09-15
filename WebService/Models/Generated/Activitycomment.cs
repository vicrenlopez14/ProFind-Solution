using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("activitycomment")]
    [Index("IdA1", Name = "FK_ActivityComment_Activity")]
    [Index("IdC5", Name = "FK_ActivityComment_Client")]
    [Index("IdP5", Name = "FK_ActivityComment_Professional")]
    public partial class Activitycomment
    {
        [Key]
        [Column("IdAC")]
        [StringLength(21)]
        public string IdAc { get; set; } = null!;
        [Column("CommentAC")]
        [StringLength(500)]
        public string? CommentAc { get; set; }
        [Column("DateAC")]
        public DateOnly? DateAc { get; set; }
        [Column("AskToCheckChatAC")]
        public bool? AskToCheckChatAc { get; set; }
        [StringLength(21)]
        public string? IdA1 { get; set; }
        [StringLength(21)]
        public string? IdP5 { get; set; }
        [StringLength(21)]
        public string? IdC5 { get; set; }

        [ForeignKey("IdA1")]
        [InverseProperty("Activitycomments")]
        public virtual Activity? IdA1Navigation { get; set; }
        [ForeignKey("IdC5")]
        [InverseProperty("Activitycomments")]
        public virtual Client? IdC5Navigation { get; set; }
        [ForeignKey("IdP5")]
        [InverseProperty("Activitycomments")]
        public virtual Professional? IdP5Navigation { get; set; }
    }
}
