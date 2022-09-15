using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("changepasswordcode")]
    [Index("IdC1", Name = "FK_Client_ChangePasswordCode")]
    public partial class Changepasswordcode
    {
        [Key]
        [Column("IdCPC")]
        [StringLength(21)]
        public string IdCpc { get; set; } = null!;
        [Column("CodeCPC")]
        [StringLength(64)]
        public string? CodeCpc { get; set; }
        [Column("VerifiedCPC")]
        public bool? VerifiedCpc { get; set; }
        [StringLength(21)]
        public string? IdC1 { get; set; }

        [ForeignKey("IdC1")]
        [InverseProperty("Changepasswordcodes")]
        public virtual Client? IdC1Navigation { get; set; }
    }
}
