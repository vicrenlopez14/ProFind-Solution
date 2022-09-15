using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("projectstatus")]
    public partial class Projectstatus
    {
        public Projectstatus()
        {
            Projects = new HashSet<Project>();
        }

        [Key]
        [Column("IdPS")]
        [StringLength(21)]
        public string IdPs { get; set; } = null!;
        [Column("NamePS")]
        [StringLength(20)]
        public string? NamePs { get; set; }
        [Column("DescriptionPS")]
        [StringLength(150)]
        public string? DescriptionPs { get; set; }
        [Column("ColorPS")]
        [StringLength(6)]
        public string? ColorPs { get; set; }

        [InverseProperty("IdPs1Navigation")]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
