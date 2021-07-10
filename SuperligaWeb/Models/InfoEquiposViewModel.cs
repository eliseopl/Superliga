using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SuperligaWeb.Models
{
    public class InfoEquiposViewModel
    {
        public int CantidadHinchas { get; set; }
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        [DisplayName("Promedio Edad")]
        public float PromedioEdad { get; set; }
        [DisplayName("Mayor Edad")]
        public int MenorEdadRegistrada { get; set; }
        [DisplayName("Menor Edad")]
        public int MayorEdadRegistrada { get; set; }

        public InfoEquiposViewModel() { }
    }
}
