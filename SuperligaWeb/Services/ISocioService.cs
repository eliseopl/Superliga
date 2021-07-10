using System.Collections.Generic;
using SuperligaWeb.Models;

namespace SuperligaWeb.Services
{
    public interface ISocioService
    {
        public List<Socio> GetSocios(string filename);
        public List<Socio> GetSocios(List<string> filenames);
    }
}
