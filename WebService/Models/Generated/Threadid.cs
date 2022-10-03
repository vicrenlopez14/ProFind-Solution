using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("threadids")]
    [Index("IdC", Name = "FK_ThreadIds_Client")]
    [Index("IdP", Name = "FK_ThreadIds_Professional")]
    public partial class Threadid
    {
        [Key]
        public string IdT { get; set; } = null!;
        [StringLength(21)]
        public string? IdC { get; set; }
        [StringLength(21)]
        public string? IdP { get; set; }

        [ForeignKey("IdC")]
        [InverseProperty("Threadids")]
        public virtual Client? IdCNavigation { get; set; }
        [ForeignKey("IdP")]
        [InverseProperty("Threadids")]
        public virtual Professional? IdPNavigation { get; set; }
    }
}
