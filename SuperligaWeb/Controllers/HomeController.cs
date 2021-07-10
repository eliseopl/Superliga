using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SuperligaWeb.Helpers;
using SuperligaWeb.Models;
using SuperligaWeb.Services;

namespace SuperligaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISocioService _socioService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger, ISocioService socioService)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _socioService = socioService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FileUpload(List<IFormFile> filesData)
        {
            var filePaths = new List<string>();
            List<Socio> socios = new List<Socio>();
            try
            {
                var filesNamesNotValid = filesData.Select(f => f.FileName).Where(fn => Path.GetExtension(fn) != ".csv");

                if (filesNamesNotValid.Count() > 0)
                {
                    return BadRequest($"Error en {string.Join(", ", filesNamesNotValid)} no se admiten archivos sin extensión CSV");
                }

                foreach (var formFile in filesData)
                {
                    if (formFile.Length > 0)
                    {
                        string filePath = await SaveFile(formFile);
                        filePaths.Add(filePath);
                    }
                }

                if (filePaths.Count() <= 0)
                {
                    return NoContent();
                }

                SetSocios(filePaths);

                int filesCount = filesData.Count();
                string successMessage = filesCount > 1 ? $"Se han procesado {filesData.Count} archivos" : $"Se ha procesado {filesData.Count} archivo";

                return Ok(new { message = successMessage });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Error();
            }
        }

        [HttpGet]
        public IActionResult RestartUpload()
        {
            _httpContextAccessor.HttpContext.Session.RemoveSocios();
            return View("Index");
        }

        private void SetSocios(List<string> filePaths)
        {
            List<Socio> socios = _socioService.GetSocios(filePaths);
            _httpContextAccessor.HttpContext.Session.SetSocios(socios);
        }

        private static async Task<string> SaveFile(IFormFile formFile)
        {
            //Al ser un ejemplo utilizo carpetas temporales.
            string filePath = Path.GetTempPath() + formFile.FileName;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return filePath;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
