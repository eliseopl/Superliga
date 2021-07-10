using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using SuperligaWeb.Models;

namespace SuperligaWeb.Helpers
{
    public static class SessionUtil
    {
        private const string SociosKey = "Socios";

        public static void SetSocios(this ISession session, List<Socio> socios)
        {
            session.SetString(SociosKey, JsonConvert.SerializeObject(socios));
        }

        public static List<Socio> GetSocios(this ISession session)
        {
            var sociosJson = session.GetString(SociosKey);
            return sociosJson == null ? null : JsonConvert.DeserializeObject<List<Socio>>(sociosJson);
        }

        public static void RemoveSocios(this ISession session)
        {
            session.Remove(SociosKey);
        }
    }
}
