using AccesoDatos.UnidadTrabajos.IUnidadTrabajos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entidades;
using Models.Utilidades;
using Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace AplicacionEYPruebaTecnica.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public HomeController(IUnidadTrabajo unidadTrabajo)
        {
            
            _unidadTrabajo = unidadTrabajo;
        }
        public async Task<IActionResult> Index()
        {

            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var claim = claimsIdentity?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            int id = Convert.ToInt32(claim.Value);

            Usuarios? usuario = await _unidadTrabajo.Usuarios
                .GetObtenerPrimero(x => x.Id == id);

            HttpContext.Session.SetString(DS.ssUsuario, usuario.UserName ?? "");

            TempData[DS.Exitosa] = null;
            TempData[DS.Error] = null;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
