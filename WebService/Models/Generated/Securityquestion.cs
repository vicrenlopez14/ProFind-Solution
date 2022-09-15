using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("securityquestion")]
    [Index("NameSq", Name = "NameSQ", IsUnique = true)]
    public partial class Securityquestion
    {
        public Securityquestion()
        {
            Securityansweradmins = new HashSet<Securityansweradmin>();
            Securityanswerclients = new HashSet<Securityanswerclient>();
            Securityanswerprofessionals = new HashSet<Securityanswerprofessional>();
        }

        [Key]
        [Column("IdSQ")]
        [StringLength(21)]
        public string IdSq { get; set; } = null!;
        [Column("NameSQ")]
        [StringLength(50)]
        public string? NameSq { get; set; }

        [InverseProperty("IdSq1Navigation")]
        public virtual ICollection<Securityansweradmin> Securityansweradmins { get; set; }
        [InverseProperty("IdSq1Navigation")]
        public virtual ICollection<Securityanswerclient> Securityanswerclients { get; set; }
        [InverseProperty("IdSqNavigation")]
        public virtual ICollection<Securityanswerprofessional> Securityanswerprofessionals { get; set; }
    }
}
