using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reporter.Models.Generated
{
    [Table("admin")]
    [Index("EmailA", Name = "EmailA", IsUnique = true)]
    [Index("IdR1", Name = "FK_Admin_Rank")]
    public partial class Admin
    {
        public Admin()
        {
            Changepasswordcodes = new HashSet<Changepasswordcode>();
        }

        [Key]
        [StringLength(21)]
        public string IdA { get; set; } = null!;
        [StringLength(50)]
        public string? NameA { get; set; }
        public string? EmailA { get; set; }
        [StringLength(15)]
        public string? TelA { get; set; }
        [StringLength(64)]
        public string? PasswordA { get; set; }
        public string? PictureA { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDateA { get; set; }
        [Column(TypeName = "int(11)")]
        public int? IdR1 { get; set; }
        [StringLength(255)]
        public string? CommunicationIdA { get; set; }

        [ForeignKey("IdR1")]
        [InverseProperty("Admins")]
        public virtual Rank? IdR1Navigation { get; set; }
        [InverseProperty("IdA1Navigation")]
        public virtual ICollection<Changepasswordcode> Changepasswordcodes { get; set; }
    }
}
