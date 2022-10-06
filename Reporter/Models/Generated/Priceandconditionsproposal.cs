using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reporter.Models.Generated
{
    [Table("priceandconditionsproposal")]
    [Index("IdPp1", Name = "FK_PriceAndConditionsProposal_Proposal")]
    public partial class Priceandconditionsproposal
    {
        [Key]
        [Column("IdPCP")]
        [StringLength(21)]
        public string IdPcp { get; set; } = null!;
        [Column("PricePCP")]
        public float? PricePcp { get; set; }
        [Column("ConditionsPCP")]
        [StringLength(500)]
        public string? ConditionsPcp { get; set; }
        [Column("IdPP1")]
        [StringLength(21)]
        public string? IdPp1 { get; set; }
        [Column("IdPJ1")]
        [StringLength(21)]
        public string? IdPj1 { get; set; }

        [ForeignKey("IdPp1")]
        [InverseProperty("Priceandconditionsproposals")]
        public virtual Proposal? IdPp1Navigation { get; set; }
    }
}
