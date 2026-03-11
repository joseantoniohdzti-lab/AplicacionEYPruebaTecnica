using AccesoDatos.UnidadTrabajos.IUnidadTrabajos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using System.Security.Claims;

namespace AplicacionEYPruebaTecnica.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ILogger<LoginController> _logger;

        public LoginController(
            IUnidadTrabajo unidadTrabajo,
            ILogger<LoginController> logger)
        {
            _unidadTrabajo = unidadTrabajo;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult LoginIndex()
        {
            var model = new LoginVM
            {
                ReturnUrl = "/Admin/Home/Index"
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> LoginIndex(LoginVM model)
        {
            try
            {

                if (string.IsNullOrEmpty(model.UserName))
                {
                    ModelState.AddModelError(string.Empty, "Usuario o contraseña inválidos.");
                    return View(model);
                }

                string paswordEncriptado = await _unidadTrabajo.Usuarios
                    .EncriptarPassword(model.Password);

                var usuario = await _unidadTrabajo.Usuarios
                    .GetObtenerPrimero(x => x.UserName.Trim() == model.UserName.Trim()
                    && x.Password.Trim() == paswordEncriptado.Trim());

                if (usuario != null)
                {

                    if (usuario.Estatus)
                    {

                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.UserName),
                    new Claim(ClaimTypes.Email, usuario.Correo ?? ""),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    // Puedes agregar más claims según necesites
                    new Claim("NombreCompleto", $"{usuario.NombreCompleto}")
                };

                        var claimsIdentity = new ClaimsIdentity(
                            claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        _logger.LogInformation($"Usuario {model.UserName} ha iniciado sesión");

                        // Redireccionar
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }

                        return RedirectToAction("Index", "Home", new { area = "Admin" });

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "El Usuario Esta Inhabilitado, Contactar al Administrador.");
                        return View(model);
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuario o contraseña inválidos.");
                    return View(model);
                }


            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                //HttpContext.Session.Clear();

                _logger.LogInformation("Sesión cerrada exitosamente");

                return RedirectToAction("LoginIndex", "Login", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

    }
}
