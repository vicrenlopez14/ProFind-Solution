using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("changepasswordcode")]
    [Index("IdA1", Name = "FK_Admin_ChangePasswordCode")]
    [Index("IdC1", Name = "FK_Client_ChangePasswordCode")]
    [Index("IdP1", Name = "FK_Professional_ChangePasswordCode")]
    public partial class Changepasswordcode
    {
        [Key]
        [Column("IdCPC")]
        [StringLength(21)]
        public string IdCpc { get; set; } = null!;
        [Column("CodeCPC")]
        [StringLength(4)]
        public string? CodeCpc { get; set; }
        [Column("ValidCPC")]
        public bool? ValidCpc { get; set; }
        [StringLength(21)]
        public string? IdC1 { get; set; }
        [StringLength(21)]
        public string? IdA1 { get; set; }
        [StringLength(21)]
        public string? IdP1 { get; set; }

        [ForeignKey("IdA1")]
        [InverseProperty("Changepasswordcodes")]
        public virtual Admin? IdA1Navigation { get; set; }
        [ForeignKey("IdC1")]
        [InverseProperty("Changepasswordcodes")]
        public virtual Client? IdC1Navigation { get; set; }
        [ForeignKey("IdP1")]
        [InverseProperty("Changepasswordcodes")]
        public virtual Professional? IdP1Navigation { get; set; }
    }
}
