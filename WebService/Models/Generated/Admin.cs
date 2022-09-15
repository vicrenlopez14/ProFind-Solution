using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("admin")]
    [Index("IdR1", Name = "FK_Admin_Rank")]
    public partial class Admin
    {
        public Admin()
        {
            Securityansweradmins = new HashSet<Securityansweradmin>();
            Supporttickets = new HashSet<Supportticket>();
        }

        [Key]
        [StringLength(21)]
        public string IdA { get; set; } = null!;
        [StringLength(50)]
        public string? NameA { get; set; }
        [StringLength(50)]
        public string? EmailA { get; set; }
        [StringLength(15)]
        public string? TelA { get; set; }
        [StringLength(64)]
        public string? PasswordA { get; set; }
        public byte[]? PictureA { get; set; }
        [Column(TypeName = "int(11)")]
        public int? IdR1 { get; set; }

        [ForeignKey("IdR1")]
        [InverseProperty("Admins")]
        public virtual Rank? IdR1Navigation { get; set; }
        [InverseProperty("IdA1Navigation")]
        public virtual ICollection<Securityansweradmin> Securityansweradmins { get; set; }
        [InverseProperty("IdA2Navigation")]
        public virtual ICollection<Supportticket> Supporttickets { get; set; }
    }
}
