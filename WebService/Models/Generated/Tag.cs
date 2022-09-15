using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("tag")]
    [Index("IdPj", Name = "FK_Tag_Project")]
    public partial class Tag
    {
        public Tag()
        {
            Activities = new HashSet<Activity>();
        }

        [Key]
        [StringLength(21)]
        public string IdT { get; set; } = null!;
        [StringLength(50)]
        public string? NameT { get; set; }
        [MaxLength(3)]
        public byte[]? ColorT { get; set; }
        [Column("IdPJ")]
        [StringLength(21)]
        public string? IdPj { get; set; }

        [ForeignKey("IdPj")]
        [InverseProperty("Tags")]
        public virtual Project? IdPjNavigation { get; set; }
        [InverseProperty("IdT1Navigation")]
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
