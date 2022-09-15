using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("securityansweradmins")]
    [Index("IdA1", Name = "FK_Admin_SecurityAnswerAdmins")]
    [Index("IdSq1", Name = "FK_SecurityAnswerAdmins_SecurityQuestion")]
    public partial class Securityansweradmin
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
        public string? IdA1 { get; set; }

        [ForeignKey("IdA1")]
        [InverseProperty("Securityansweradmins")]
        public virtual Admin? IdA1Navigation { get; set; }
        [ForeignKey("IdSq1")]
        [InverseProperty("Securityansweradmins")]
        public virtual Securityquestion? IdSq1Navigation { get; set; }
    }
}
