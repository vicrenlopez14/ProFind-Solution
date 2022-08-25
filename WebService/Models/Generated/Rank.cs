﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models
{
    [Table("rank")]
    [Index("NameR", Name = "NameR", IsUnique = true)]
    public partial class Rank
    {
        [Key]
        public int IdR { get; set; }
        [StringLength(50)]
        public string? NameR { get; set; }
    }
}