using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("proposalnotification")]
    [Index("IdC4", Name = "FK_ProposalNotification_Client")]
    [Index("IdP4", Name = "FK_ProposalNotification_Professional")]
    [Index("IdPp1", Name = "FK_ProposalNotification_Proposal")]
    public partial class Proposalnotification
    {
        [Key]
        [Column("IdPN")]
        [StringLength(21)]
        public string IdPn { get; set; } = null!;
        [Column("ContentPN")]
        [StringLength(500)]
        public string? ContentPn { get; set; }
        [Column("ImagePN")]
        public byte[]? ImagePn { get; set; }
        [Column("ActionPN", TypeName = "enum('chat','resend','support','checktoapprove')")]
        public string? ActionPn { get; set; }
        [Column("IdPP1")]
        [StringLength(21)]
        public string? IdPp1 { get; set; }
        [StringLength(21)]
        public string? IdP4 { get; set; }
        [StringLength(21)]
        public string? IdC4 { get; set; }

        [ForeignKey("IdC4")]
        [InverseProperty("Proposalnotifications")]
        public virtual Client? IdC4Navigation { get; set; }
        [ForeignKey("IdP4")]
        [InverseProperty("Proposalnotifications")]
        public virtual Professional? IdP4Navigation { get; set; }
        [ForeignKey("IdPp1")]
        [InverseProperty("Proposalnotifications")]
        public virtual Proposal? IdPp1Navigation { get; set; }
    }
}
