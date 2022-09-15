using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("securityanswerclients")]
    [Index("IdC1", Name = "FK_Client_SecurityAnswerClients")]
    [Index("IdSq1", Name = "FK_SecurityAnswerClients_SecurityQuestion")]
    public partial class Securityanswerclient
    {
        [Key]
        [Column("IdSA")]
        [StringLength(21)]
        public string IdSa { get; set; } = null!;
        [Column("AnswerSA")]
        [StringLength(50)]
        public string? AnswerSa { get; set; }
        [Column("IdSQ1")]
        [StringLength(21)]
        public string? IdSq1 { get; set; }
        [StringLength(21)]
        public string? IdC1 { get; set; }

        [ForeignKey("IdC1")]
        [InverseProperty("Securityanswerclients")]
        public virtual Client? IdC1Navigation { get; set; }
        [ForeignKey("IdSq1")]
        [InverseProperty("Securityanswerclients")]
        public virtual Securityquestion? IdSq1Navigation { get; set; }
    }
}
