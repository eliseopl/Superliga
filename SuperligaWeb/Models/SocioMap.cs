using CsvHelper.Configuration;

namespace SuperligaWeb.Models
{
    public class SocioMap : ClassMap<Socio>
    {
        public SocioMap()
        {
            Map(m => m.Nombre).Index(0);
            Map(m => m.Edad).Index(1);
            Map(m => m.Equipo).Index(2);
            Map(m => m.EstadoCivil).Index(3);
            Map(m => m.NivelEstudios).Index(4);
        }
    }
}
