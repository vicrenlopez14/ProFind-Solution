using System;

namespace ProFind.Lib.Global.Models
{
    public class PFProfessional
    {
        public PFProfessional(string id, string name, DateTime datebirth, string email,
            PFCurriculum curriculum, PFJob job)
        {
            Id = id;
            Name = name;
            DateBirth = datebirth;
            Email = email;
            Curriculum = curriculum;
            Job = job;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Email { get; set; }
        public PFCurriculum Curriculum { get; set; }
        public PFJob Job { get; set; }
    }
}