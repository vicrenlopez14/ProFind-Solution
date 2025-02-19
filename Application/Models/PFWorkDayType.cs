﻿using Application.Services;
using Newtonsoft.Json;
using Nito.AsyncEx.Synchronous;
using System;
using System.Threading.Tasks;

namespace Application.Models
{
    public class PFWorkDayType
    {
        [JsonConstructor]
        public PFWorkDayType(string idWdt, string nameWdt)
        {
            IdWDT = idWdt;
            NameWDT = nameWdt;
        }

        public PFWorkDayType()
        {
        }

        public async void FillFromId(string id)
        {
            var result = await new PFWorkDayTypeService().GetObjectAsync(id);

            IdWDT = result.IdWDT;
            NameWDT = result.NameWDT;
        }

        public string IdWDT { get; set; }
        public string NameWDT { get; set; }

        public Task Create(PFWorkDayType newObject)
        {
            throw new NotImplementedException();
        }
    }
}