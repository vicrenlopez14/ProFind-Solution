﻿using System;
using System.Collections.Generic;
using Application.Services;
using Newtonsoft.Json;
using Nito.AsyncEx.Synchronous;

namespace Application.Models
{
    public class PFProject
    {
        [JsonConstructor]
        public PFProject(string idPs1, string idPj, string titlePj, string descriptionPj, PFProjectStatus status,
            string idP1, PFProfessional responsibleProfessional, string idC1, PFClient responsibleClient,
            byte[] picturePj, float totalPricePj, IEnumerable<PFActivity> activities)
        {
            _idPS1 = idPs1;
            IdPJ = idPj;
            TitlePJ = titlePj;
            DescriptionPJ = descriptionPj;
            Status = status;
            IdP1 = idP1;
            ResponsibleProfessional = responsibleProfessional;
            IdC1 = idC1;
            ResponsibleClient = responsibleClient;
            PicturePJ = picturePj;
            TotalPricePJ = totalPricePj;
            Activities = activities;
        }


        public PFProject()
        {
        }

        public async void FillFromId(string id)
        {
            var result = await new PfProjectService().GetObjectAsync(id);

            IdPJ = result.IdPJ;
            TitlePJ = result.TitlePJ;
            DescriptionPJ = result.DescriptionPJ;
            Status = result.Status;
            PicturePJ = result.PicturePJ;
            TotalPricePJ = result.TotalPricePJ;
            IdP1 = result.IdP1;
            if (!string.IsNullOrEmpty(IdP1))
                ResponsibleProfessional.FillFromId(IdP1);
            IdPS1 = result.IdPS1;
            IdC1 = result.IdC1;
            if (!string.IsNullOrEmpty(IdC1))
                ResponsibleClient.FillFromId(IdC1);
        }

        public string IdPJ { get; set; }

        public string TitlePJ { get; set; }
        public string DescriptionPJ { get; set; }

        public string _idPS1;

        public string IdPS1
        {
            get => _idPS1;
            set
            {
                _idPS1 = value;
                try
                {
                    Status = _idPS1 == null || int.Parse(_idPS1) == 1
                        ? PFProjectStatus.Inactive
                        : PFProjectStatus.Active;
                }
                catch (Exception e)
                {
                    Status = PFProjectStatus.Inactive;
                }
            }
        }

        public PFProjectStatus Status { get; set; }

        public string IdP1 { get; set; }
        public PFProfessional ResponsibleProfessional { get; set; }

        public string IdC1 { get; set; }
        public PFClient ResponsibleClient { get; set; }

        public byte[] PicturePJ { get; set; }

        public float TotalPricePJ { get; set; }

        public IEnumerable<PFActivity> Activities { get; set; }
    }
}