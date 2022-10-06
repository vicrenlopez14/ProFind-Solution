using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("graphicpreference")]
    public partial class Graphicpreference
    {
        [Key]
        [Column("IdPRF")]
        public string IdPrf { get; set; } = null!;
        [Column("NamePRF")]
        [StringLength(50)]
        public string? NamePrf { get; set; }
        [Column("DescriptionPRF")]
        [StringLength(255)]
        public string? DescriptionPrf { get; set; }
        [Column("PicturePRF")]
        public string? PicturePrf { get; set; }
        [Column("BannerPRF")]
        public string? BannerPrf { get; set; }
    }
}
