using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("supportticket")]
    [Index("IdAct1", Name = "FK_SupportTicket_Activity")]
    [Index("IdA2", Name = "FK_SupportTicket_Admin")]
    [Index("IdC5", Name = "FK_SupportTicket_Client")]
    [Index("IdP5", Name = "FK_SupportTicket_Professional")]
    [Index("IdPj4", Name = "FK_SupportTicket_Project")]
    [Index("IdPpy1", Name = "FK_SupportTicket_ProjectPay")]
    [Index("IdPp2", Name = "FK_SupportTicket_Proposal")]
    public partial class Supportticket
    {
        [Key]
        [Column("IdST")]
        [StringLength(21)]
        public string IdSt { get; set; } = null!;
        [Column("TitleST")]
        [StringLength(50)]
        public string? TitleSt { get; set; }
        [Column("ContentST")]
        [StringLength(500)]
        public string? ContentSt { get; set; }
        [Column("ImageST")]
        public byte[]? ImageSt { get; set; }
        [Column("DocumentST")]
        public byte[]? DocumentSt { get; set; }
        [Column("LocationST")]
        [StringLength(100)]
        public string? LocationSt { get; set; }
        [Column("AudioST")]
        public byte[]? AudioSt { get; set; }
        [Column("DateTimeST")]
        public DateOnly? DateTimeSt { get; set; }
        [Column("TicketStatusST", TypeName = "enum('taken','pending','actiontaken')")]
        public string? TicketStatusSt { get; set; }
        [Column("SuggestedActionST", TypeName = "enum('checkproject','checkactivity','checkproposal','checkpayment','checkchat','closeticket')")]
        public string? SuggestedActionSt { get; set; }
        [Column("ResponseContentST")]
        [StringLength(500)]
        public string? ResponseContentSt { get; set; }
        [Column("ResponseImageST")]
        public byte[]? ResponseImageSt { get; set; }
        [Column("ResponseDocumentST")]
        public byte[]? ResponseDocumentSt { get; set; }
        [Column("ResponseLocationST")]
        [StringLength(100)]
        public string? ResponseLocationSt { get; set; }
        [Column("ResponseAudioST")]
        public byte[]? ResponseAudioSt { get; set; }
        [StringLength(21)]
        public string? IdP5 { get; set; }
        [StringLength(21)]
        public string? IdC5 { get; set; }
        [StringLength(21)]
        public string? IdA2 { get; set; }
        [Column("IdPJ4")]
        [StringLength(21)]
        public string? IdPj4 { get; set; }
        [Column("IdACT1")]
        [StringLength(21)]
        public string? IdAct1 { get; set; }
        [Column("IdPP2")]
        [StringLength(21)]
        public string? IdPp2 { get; set; }
        [Column("IdPPY1")]
        [StringLength(21)]
        public string? IdPpy1 { get; set; }

        [ForeignKey("IdA2")]
        [InverseProperty("Supporttickets")]
        public virtual Admin? IdA2Navigation { get; set; }
        [ForeignKey("IdAct1")]
        [InverseProperty("Supporttickets")]
        public virtual Activity? IdAct1Navigation { get; set; }
        [ForeignKey("IdC5")]
        [InverseProperty("Supporttickets")]
        public virtual Client? IdC5Navigation { get; set; }
        [ForeignKey("IdP5")]
        [InverseProperty("Supporttickets")]
        public virtual Professional? IdP5Navigation { get; set; }
        [ForeignKey("IdPj4")]
        [InverseProperty("Supporttickets")]
        public virtual Project? IdPj4Navigation { get; set; }
        [ForeignKey("IdPp2")]
        [InverseProperty("Supporttickets")]
        public virtual Proposal? IdPp2Navigation { get; set; }
        [ForeignKey("IdPpy1")]
        [InverseProperty("Supporttickets")]
        public virtual Projectpay? IdPpy1Navigation { get; set; }
    }
}
