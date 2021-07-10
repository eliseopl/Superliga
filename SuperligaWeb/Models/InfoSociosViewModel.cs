using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static SuperligaWeb.Models.SociosConstants;

namespace SuperligaWeb.Models
{
    public class InfoSociosViewModel
    {
        [DisplayName("Personas Registradas")]
        public int CantPersonasRegistradas { get; set; }
        [DisplayName("Promedio de edad hinchas de Racing")]
        public string PromedioEdadHinchasDeRacing { get; set; }
        [DisplayName("Nombres mas comunes hinchas de River")]
        public List<string> NombresComunesHinchasDeRiver { get; set; }

        public InfoSociosViewModel() { }

        public InfoSociosViewModel(List<Socio> socios)
        {
            this.CantPersonasRegistradas = socios.Count();
            this.PromedioEdadHinchasDeRacing = $"{GetPromedioEdadRacing(socios)} años";
            this.NombresComunesHinchasDeRiver = GetNombresComunesHinchasDeRiver(socios);
        }

        private List<string> GetNombresComunesHinchasDeRiver(List<Socio> socios)
        {
            var nombres = socios.Where(s => s.Equipo == Equipos.RIVER)
                .GroupBy(s => s.Nombre)
                .Select(x => new
                {
                    Count = x.Count(),
                    Nombre = x.Key
                })
                .OrderByDescending(x => x.Count)
                .Select(x => x.Nombre)
                .Take(5)
                .ToList();

            return nombres;
        }

        private static float GetPromedioEdadRacing(List<Socio> socios)
        {
            return (float)Math.Round(socios.Where(s => s.Equipo == Equipos.RACING).Average(s => s.Edad), 1);
        }
    }
}
