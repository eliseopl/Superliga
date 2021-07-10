using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperligaWeb.Models;

namespace SuperligaWeb.Services
{
    public class SocioService : ISocioService
    {
        public List<Socio> GetSocios(string filename)
        {
            var lines = new List<Socio>();
            var config = new CsvConfiguration(new CultureInfo("es"))
            {
                Encoding = Encoding.GetEncoding("ISO-8859-1"),
                HasHeaderRecord = false,
                Delimiter = ";"
            };
            using (var fs = new StreamReader(filename, Encoding.GetEncoding("ISO-8859-1")))
            using (var csv = new CsvHelper.CsvReader(fs, config))
            {
                csv.Context.RegisterClassMap<SocioMap>();
                lines = csv.GetRecords<Socio>().ToList();
            }

            return lines;
        }

        public List<Socio> GetSocios(List<string> filenames)
        {
            List<Socio> socios = new List<Socio>();
            foreach(var filename in filenames)
            {
                socios.AddRange(GetSocios(filename));
            }
            return socios;
        }
    }
}
