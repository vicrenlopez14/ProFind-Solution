using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("professional")]
    [Index("IdDp1", Name = "FK_Professional_Department")]
    [Index("IdPfs1", Name = "FK_Professional_Profession")]
    public partial class Professional
    {
        public Professional()
        {
            Activitycomments = new HashSet<Activitycomment>();
            Messages = new HashSet<Message>();
            Projectpays = new HashSet<Projectpay>();
            Projects = new HashSet<Project>();
            Projecttemplates = new HashSet<Projecttemplate>();
            Proposalnotifications = new HashSet<Proposalnotification>();
            Proposals = new HashSet<Proposal>();
            Securityanswerprofessionals = new HashSet<Securityanswerprofessional>();
            Supporttickets = new HashSet<Supportticket>();
        }

        [Key]
        [StringLength(21)]
        public string IdP { get; set; } = null!;
        [StringLength(50)]
        public string? NameP { get; set; }
        public DateOnly? DateBirthP { get; set; }
        [StringLength(21)]
        public string? EmailP { get; set; }
        [StringLength(64)]
        public string? PasswordP { get; set; }
        public bool? ActiveP { get; set; }
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
        public DateOnly? HiringDateP { get; set; }
        public byte[]? PictureP { get; set; }
        public byte[]? CurriculumP { get; set; }
        public double? LatitudeLocationP { get; set; }
        public double? LongitudeLocationP { get; set; }
        [Column("IdPFS1", TypeName = "int(11)")]
        public int? IdPfs1 { get; set; }
        [Column("IdDP1", TypeName = "int(11)")]
        public int? IdDp1 { get; set; }
        [Column("IdWDT1", TypeName = "int(11)")]
        public int? IdWdt1 { get; set; }

        [ForeignKey("IdDp1")]
        [InverseProperty("Professionals")]
        public virtual Department? IdDp1Navigation { get; set; }
        [ForeignKey("IdPfs1")]
        [InverseProperty("Professionals")]
        public virtual Profession? IdPfs1Navigation { get; set; }
        [InverseProperty("IdP5Navigation")]
        public virtual ICollection<Activitycomment> Activitycomments { get; set; }
        [InverseProperty("IdP4Navigation")]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty("IdP3Navigation")]
        public virtual ICollection<Projectpay> Projectpays { get; set; }
        [InverseProperty("IdP1Navigation")]
        public virtual ICollection<Project> Projects { get; set; }
        [InverseProperty("IdP1Navigation")]
        public virtual ICollection<Projecttemplate> Projecttemplates { get; set; }
        [InverseProperty("IdP4Navigation")]
        public virtual ICollection<Proposalnotification> Proposalnotifications { get; set; }
        [InverseProperty("IdP3Navigation")]
        public virtual ICollection<Proposal> Proposals { get; set; }
        [InverseProperty("IdPNavigation")]
        public virtual ICollection<Securityanswerprofessional> Securityanswerprofessionals { get; set; }
        [InverseProperty("IdP5Navigation")]
        public virtual ICollection<Supportticket> Supporttickets { get; set; }
    }
}
