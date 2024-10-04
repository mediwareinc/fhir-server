﻿using Hl7.Fhir.Model;
using WellSky.Hss.Fhir.Features.Storage.DataModels;

namespace WellSky.Hss.Fhir.Features.Storage.Mappers
{
    internal class ConsumerMapper
    {
        public static Patient Map(Consumer consumer)
        {
            // Aldo: Map and return FHIR model from A&D data
            return new Patient
            {
                Id = consumer.Id.ToString(),
                Name =
                [
                    new HumanName
                    {
                        Given = ["test name"]
                    }
                ]
            };
        }
    }
}
