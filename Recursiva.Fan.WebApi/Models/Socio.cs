using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using IndexAttribute = CsvHelper.Configuration.Attributes.IndexAttribute;

namespace Recursiva.Fan.WebApi.Models
{

    public class Socio
    {
        [Index(0)]
        public string Name { get; set; }

        [Index(1)]
        public byte Age { get; set; }

        [Index(2)]
        public string Team { get; set; }
        
        [Index(3)]
        public string Status { get; set; }
        
        [Index(4)]
        public string Level { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

    }


    public sealed class SocioMap : ClassMap<Socio>
    {
        public SocioMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Id).Ignore();
        }
    }
}
