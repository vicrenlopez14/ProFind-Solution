using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("projectpay")]
    [Index("IdC3", Name = "FK_ProjectPay_Client")]
    [Index("IdP3", Name = "FK_ProjectPay_Professional")]
    [Index("IdPj3", Name = "FK_ProjectPay_Project")]
    public partial class Projectpay
    {
        public Projectpay()
        {
            Supporttickets = new HashSet<Supportticket>();
        }

        [Key]
        [Column("IdPPY")]
        [StringLength(21)]
        public string IdPpy { get; set; } = null!;
        [Column("PercentagePPY")]
        public float? PercentagePpy { get; set; }
        [Column("ConceptPPY")]
        [StringLength(500)]
        public string? ConceptPpy { get; set; }
        [Column("AmountPPY")]
        public float? AmountPpy { get; set; }
        [Column("CurrencyPPY")]
        [StringLength(3)]
        public string? CurrencyPpy { get; set; }
        [Column("HasLimitDatePPY")]
        public bool? HasLimitDatePpy { get; set; }
        [Column("LimitDatePPY")]
        public DateOnly? LimitDatePpy { get; set; }
        [Column("DefaultAmountPPY")]
        public float? DefaultAmountPpy { get; set; }
        [Column("PayStatusPPY", TypeName = "enum('pending','done','rejected')")]
        public string? PayStatusPpy { get; set; }
        [StringLength(21)]
        public string? IdP3 { get; set; }
        [StringLength(21)]
        public string? IdC3 { get; set; }
        [Column("IdPJ3")]
        [StringLength(21)]
        public string? IdPj3 { get; set; }

        [ForeignKey("IdC3")]
        [InverseProperty("Projectpays")]
        public virtual Client? IdC3Navigation { get; set; }
        [ForeignKey("IdP3")]
        [InverseProperty("Projectpays")]
        public virtual Professional? IdP3Navigation { get; set; }
        [ForeignKey("IdPj3")]
        [InverseProperty("Projectpays")]
        public virtual Project? IdPj3Navigation { get; set; }
        [InverseProperty("IdPpy1Navigation")]
        public virtual ICollection<Supportticket> Supporttickets { get; set; }
    }
}
