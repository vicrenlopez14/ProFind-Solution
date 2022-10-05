using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Reporter.Models.Generated
{
    [Table("professional")]
    [Index("IdDp1", Name = "FK_Professional_Department")]
    [Index("IdPfs1", Name = "FK_Professional_Profession")]
    public partial class Professional
    {
        public Professional()
        {
            Changepasswordcodes = new HashSet<Changepasswordcode>();
            Notifications = new HashSet<Notification>();
            Projects = new HashSet<Project>();
            Proposals = new HashSet<Proposal>();
            Threadids = new HashSet<Threadid>();
        }

        [Key]
        [StringLength(21)]
        public string IdP { get; set; } = null!;
        [StringLength(50)]
        public string? NameP { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateBirthP { get; set; }
        [StringLength(255)]
        public string? EmailP { get; set; }
        [StringLength(64)]
        public string? PasswordP { get; set; }
        public bool? ActiveP { get; set; }
        [StringLength(15)]
        public string? PhoneP { get; set; }
        public bool? SexP { get; set; }
        [Column("DUIP")]
        [StringLength(15)]
        public string? Duip { get; set; }
        [Column("AFPP")]
        [StringLength(50)]
        public string? Afpp { get; set; }
        [Column("ISSSP")]
        [StringLength(50)]
        public string? Isssp { get; set; }
        [StringLength(10)]
        public string? ZipCodeP { get; set; }
        public float? SalaryP { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HiringDateP { get; set; }
        public string? PictureP { get; set; }
        public byte[]? CurriculumP { get; set; }
        [StringLength(255)]
        public string? CommunicationIdP { get; set; }
        [Column("IdPFS1", TypeName = "int(11)")]
        public int? IdPfs1 { get; set; }
        [Column("IdDP1", TypeName = "int(11)")]
        public int? IdDp1 { get; set; }

        [ForeignKey("IdDp1")]
        [InverseProperty("Professionals")]
        public virtual Department? IdDp1Navigation { get; set; }
        [ForeignKey("IdPfs1")]
        [InverseProperty("Professionals")]
        public virtual Profession? IdPfs1Navigation { get; set; }
        [InverseProperty("IdP1Navigation")]
        public virtual ICollection<Changepasswordcode> Changepasswordcodes { get; set; }
        [InverseProperty("IdP1Navigation")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("IdP1Navigation")]
        public virtual ICollection<Project> Projects { get; set; }
        [InverseProperty("IdP3Navigation")]
        public virtual ICollection<Proposal> Proposals { get; set; }
        [InverseProperty("IdPNavigation")]
        public virtual ICollection<Threadid> Threadids { get; set; }
    }
}
