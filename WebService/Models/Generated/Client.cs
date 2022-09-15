using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebService.Models.Generated
{
    [Table("client")]
    public partial class Client
    {
        public Client()
        {
            Activitycomments = new HashSet<Activitycomment>();
            Changepasswordcodes = new HashSet<Changepasswordcode>();
            Messages = new HashSet<Message>();
            Projectpays = new HashSet<Projectpay>();
            Projects = new HashSet<Project>();
            Proposalnotifications = new HashSet<Proposalnotification>();
            Proposals = new HashSet<Proposal>();
            Securityanswerclients = new HashSet<Securityanswerclient>();
            Supporttickets = new HashSet<Supportticket>();
        }

        [Key]
        [StringLength(21)]
        public string IdC { get; set; } = null!;
        [StringLength(50)]
        public string? NameC { get; set; }
        [StringLength(50)]
        public string? EmailC { get; set; }
        [StringLength(64)]
        public string? PasswordC { get; set; }
        public byte[]? PictureC { get; set; }

        [InverseProperty("IdC5Navigation")]
        public virtual ICollection<Activitycomment> Activitycomments { get; set; }
        [InverseProperty("IdC1Navigation")]
        public virtual ICollection<Changepasswordcode> Changepasswordcodes { get; set; }
        [InverseProperty("IdC4Navigation")]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty("IdC3Navigation")]
        public virtual ICollection<Projectpay> Projectpays { get; set; }
        [InverseProperty("IdC1Navigation")]
        public virtual ICollection<Project> Projects { get; set; }
        [InverseProperty("IdC4Navigation")]
        public virtual ICollection<Proposalnotification> Proposalnotifications { get; set; }
        [InverseProperty("IdC3Navigation")]
        public virtual ICollection<Proposal> Proposals { get; set; }
        [InverseProperty("IdC1Navigation")]
        public virtual ICollection<Securityanswerclient> Securityanswerclients { get; set; }
        [InverseProperty("IdC5Navigation")]
        public virtual ICollection<Supportticket> Supporttickets { get; set; }
    }
}
