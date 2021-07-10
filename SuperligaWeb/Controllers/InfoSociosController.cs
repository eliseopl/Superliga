using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using SuperligaWeb.Helpers;
using SuperligaWeb.Models;
using static SuperligaWeb.Models.SociosConstants;

namespace SuperligaWeb.Controllers
{
    public class InfoSociosController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InfoSociosController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var socios = GetSocios();

            if (socios == null || socios.Count <= 0)
                return RedirectToAction("Index", "Home");

            InfoSociosViewModel infoSociosViewModel = GetInfoSociosViewModel(socios);

            return View(infoSociosViewModel);
        }

        [HttpGet]
        public IActionResult InfoEquipos()
        {
            var socios = GetSocios();

            if (socios == null || socios.Count <= 0)
                return RedirectToAction("Index", "Home");

            List<InfoEquiposViewModel> infoEquiposViewModels = GetInfoEquipos(socios.Distinct().ToList());

            return View(infoEquiposViewModels);
        }

        [HttpGet]
        public IActionResult SociosCasadosUniversitarios()
        {
            var socios = GetSocios();

            if (socios == null || socios.Count <= 0)
                return RedirectToAction("Index", "Home");

            List<SocioCasadoUniversitarioViewModel> socioCasadoUniversitarios = GetSociosCasadosUniversitarios(socios.Distinct().ToList());

            return View(socioCasadoUniversitarios);
        }

        [HttpGet]
        public IActionResult GetSociosViewModel()
        {
            var socios = _httpContextAccessor.HttpContext.Session.GetSocios().Take(100).ToList();
            return Json(new { data = socios });
        }

        [HttpGet]
        public IActionResult BackHome()
        {
            _httpContextAccessor.HttpContext.Session.RemoveSocios();
            return RedirectToAction("Home", "Index");
        }

        private List<Socio> GetSocios()
        {
            ISession session = _httpContextAccessor.HttpContext.Session;
            var socios = session.GetSocios();
            return socios;
        }

        private InfoSociosViewModel GetInfoSociosViewModel(List<Socio> socios)
        {
            InfoSociosViewModel infoSocios = new InfoSociosViewModel(socios.Distinct().ToList());
            return infoSocios;
        }

        private List<InfoEquiposViewModel> GetInfoEquipos(List<Socio> socios)
        {
            var infoEquipos = socios.GroupBy(s => s.Equipo, s => s.Edad)
                .Select(e => new InfoEquiposViewModel
                {
                    CantidadHinchas = e.Count(),
                    Nombre = e.Key,
                    PromedioEdad = (float)Math.Round(e.Average(edad => edad), 1),
                    MenorEdadRegistrada = e.Min(edad => edad),
                    MayorEdadRegistrada = e.Max(edad => edad)
                })
                .OrderByDescending(e => e.CantidadHinchas)
                .ToList();

            return infoEquipos;
        }

        private List<SocioCasadoUniversitarioViewModel> GetSociosCasadosUniversitarios(List<Socio> socios)
        {
            var infoEquipos = socios.Where(s => s.EstadoCivil == EstadosCiviles.CASADO && s.NivelEstudios == Estudios.UNIVERSITARIO).Take(100)
                .OrderBy(s => s.Edad)
                .Select(s => new SocioCasadoUniversitarioViewModel(s))
                .ToList();

            return infoEquipos;
        }
    }
}
