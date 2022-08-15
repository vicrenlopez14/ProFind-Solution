﻿using System.Collections.Generic;
using Application.Services;
using Newtonsoft.Json;

namespace Application.Models
{
    public class PFProject
    {
        [JsonConstructor]
        public PFProject(string idPj, string titlePj, string descriptionPj, byte[] picturePj)
        {
            IdPJ = idPj;
            TitlePJ = titlePj;
            DescriptionPJ = descriptionPj;
            PicturePJ = picturePj;
        }

        public PFProject()
        {
        }

        public static PFProject Initialize(string id)
        {
            var infoTask = new PfProjectService().GetObjectAsync(id);
            infoTask.Wait();

            return infoTask.Result;
        }

        private string _idPJ;

        public string IdPJ
        {
            get => _idPJ;
            set
            {
                _idPJ = value;
                // Get project activities
                Activities = PFActivity.InitializeFromProjectId(_idPJ);
            }
        }

        public string TitlePJ { get; set; }
        public string DescriptionPJ { get; set; }

        public string IdPS1
        {
            get { return IdPJ; }
            set
            {
                IdPS1 = value;
                Status = int.Parse(IdPS1) == 1 ? PFProjectStatus.Inactive : PFProjectStatus.Active;
            }
        }

        public PFProjectStatus Status { get; set; }

        private string _idP1;

        public string IdP1
        {
            get => _idP1;
            set
            {
                _idP1 = value;
                ResponsibleProfessional = PFProfessional.Initialize(_idP1);
            }
        }

        public PFProfessional ResponsibleProfessional { get; set; }

        private string _idC1;

        public string IdC1
        {
            get => _idC1;
            set
            {
                _idC1 = value;
                ResponsibleClient = PFClient.Initialize(_idC1);
            }
        }

        public PFClient ResponsibleClient { get; set; }

        public byte[] PicturePJ { get; set; }

        public IEnumerable<PFActivity> Activities { get; set; }
    }
}