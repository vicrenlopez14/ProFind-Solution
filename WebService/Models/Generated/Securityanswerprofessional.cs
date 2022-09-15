using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("securityanswerprofessionals")]
    [Index("IdP", Name = "FK_Professional_SecurityAnswerProfessionals")]
    [Index("IdSq", Name = "FK_SecurityAnswerProfessionals_SecurityQuestion")]
    public partial class Securityanswerprofessional
    {
        [Key]
        [Column("IdSA")]
        [StringLength(21)]
        public string IdSa { get; set; } = null!;
        [Column("AnswerSA")]
        [StringLength(50)]
        public string? AnswerSa { get; set; }
        [Column("IdSQ")]
        [StringLength(21)]
        public string? IdSq { get; set; }
        [StringLength(21)]
        public string? IdP { get; set; }

        [ForeignKey("IdP")]
        [InverseProperty("Securityanswerprofessionals")]
        public virtual Professional? IdPNavigation { get; set; }
        [ForeignKey("IdSq")]
        [InverseProperty("Securityanswerprofessionals")]
        public virtual Securityquestion? IdSqNavigation { get; set; }
    }
}
