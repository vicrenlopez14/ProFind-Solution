using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("rank")]
    [Index("NameR", Name = "NameR", IsUnique = true)]
    public partial class Rank
    {
        public Rank()
        {
            Admins = new HashSet<Admin>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int IdR { get; set; }
        [StringLength(50)]
        public string? NameR { get; set; }

        [InverseProperty("IdR1Navigation")]
        public virtual ICollection<Admin> Admins { get; set; }
    }
}
