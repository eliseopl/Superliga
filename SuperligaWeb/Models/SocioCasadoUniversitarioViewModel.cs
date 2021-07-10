namespace SuperligaWeb.Models
{
    public class SocioCasadoUniversitarioViewModel
    {
        public string Nombre { get; set; }
        public string Edad { get; set; }
        public string Equipo { get; set; }

        public SocioCasadoUniversitarioViewModel() { }

        public SocioCasadoUniversitarioViewModel(Socio socio)
        {
            this.Nombre = socio.Nombre;
            this.Edad = $"{socio.Edad} años";
            this.Equipo = socio.Equipo;
        }
    }
}
