using System.ComponentModel;

namespace SuperligaWeb.Models
{
    public class Socio
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Equipo { get; set; }
        [DisplayName("Estado civil")]
        public string EstadoCivil { get; set; }
        [DisplayName("Nivel de Estudios")]
        public string NivelEstudios { get; set; }
    }

    public static class SociosConstants 
    {
        public static class EstadosCiviles
        {
            public const string CASADO = "Casado";
            public const string SOLTERO = "Soltero";
        }

        public static class Equipos
        {
            public const string RACING = "Racing";
            public const string RIVER = "River";
        }

        public static class Estudios
        {
            public const string UNIVERSITARIO = "Universitario";
            public const string SECUNDARIO = "Secundario";
        }
    }
}
