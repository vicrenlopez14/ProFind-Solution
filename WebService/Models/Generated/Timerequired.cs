using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("timerequired")]
    public partial class Timerequired
    {
        public Timerequired()
        {
            Projects = new HashSet<Project>();
        }

        [Key]
        [Column("IdTR", TypeName = "int(11)")]
        public int IdTr { get; set; }
        [Column("NameTR")]
        [StringLength(50)]
        public string? NameTr { get; set; }

        [InverseProperty("TimeRequiredTr1Navigation")]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
