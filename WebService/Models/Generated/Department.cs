using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("department")]
    public partial class Department
    {
        public Department()
        {
            Professionals = new HashSet<Professional>();
        }

        [Key]
        [Column("IdDP", TypeName = "int(11)")]
        public int IdDp { get; set; }
        [Column("NameDP")]
        [StringLength(30)]
        public string? NameDp { get; set; }

        [InverseProperty("IdDp1Navigation")]
        public virtual ICollection<Professional> Professionals { get; set; }
    }
}
