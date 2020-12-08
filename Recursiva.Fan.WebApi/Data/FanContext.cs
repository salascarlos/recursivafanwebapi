using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Recursiva.Fan.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursiva.Fan.WebApi.Data
{
    public class FanContext : DbContext
    {
        public DbSet<Socio> Socios { get; set; }

        public FanContext(DbContextOptions options) : base(options)
        {
            LoadSocios();
        }

        public void LoadSocios()
        {
            List<Socio> socios = new List<Socio>();

            Encoding ascii = Encoding.GetEncoding("iso-8859-1");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", "socio.csv");

            using (var reader = new StreamReader(path, ascii))

            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.Delimiter = ";";
                csv.Configuration.RegisterClassMap<SocioMap>();

                socios = csv.GetRecords<Socio>().ToList();
            }

            Socios.AddRange(socios);
        }

        public List<Socio> GetSocios()
        {
            return Socios.Local.ToList<Socio>();
        }
    }
}
