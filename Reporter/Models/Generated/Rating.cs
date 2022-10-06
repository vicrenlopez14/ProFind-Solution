using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reporter.Models.Generated
{
    [Table("rating")]
    [Index("IdC1", Name = "FK_Rating_Client")]
    [Index("IdP1", Name = "FK_Rating_Professional")]
    public partial class Rating
    {
        [Key]
        [Column("IdRT")]
        [StringLength(21)]
        public string IdRt { get; set; } = null!;
        [Column("RatingRT")]
        public float? RatingRt { get; set; }
        [Column("CommentRT")]
        [StringLength(500)]
        public string? CommentRt { get; set; }
        [StringLength(21)]
        public string? IdP1 { get; set; }
        [StringLength(21)]
        public string? IdC1 { get; set; }

        [ForeignKey("IdC1")]
        [InverseProperty("Ratings")]
        public virtual Client? IdC1Navigation { get; set; }
        [ForeignKey("IdP1")]
        [InverseProperty("Ratings")]
        public virtual Professional? IdP1Navigation { get; set; }
    }
}
