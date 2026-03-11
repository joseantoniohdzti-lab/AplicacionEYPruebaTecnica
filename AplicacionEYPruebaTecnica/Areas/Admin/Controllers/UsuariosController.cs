using AccesoDatos.UnidadTrabajos;
using AccesoDatos.UnidadTrabajos.IUnidadTrabajos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entidades;
using Models.EntidadesDtos;
using Models.Utilidades;
using System.Runtime.CompilerServices;

namespace AplicacionEYPruebaTecnica.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly IUnidadTrabajo _UnidadTrabajo;
        private readonly IMapper _mapper;
        public UsuariosController(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _UnidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        public IActionResult UsuariosIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UsuariosUpsert([FromBody] AddUsuariosDto model)
        {
            Usuarios? modelUsuario = new Usuarios();
            bool bandera = false;
            string mss = "";
            try
            {

                if (model.Id != 0)
                {
                    ModelState.Remove("Password");
                }

                if (ModelState.IsValid)
                {
                    modelUsuario = _mapper.Map<Usuarios>(model);

                    if (model.Id != 0)
                    {
                        var usuariodb = await _UnidadTrabajo.Usuarios
                            .GetObtener(model.Id);

                        if (usuariodb.Password != modelUsuario.Password)
                        {
                            modelUsuario.Password = await _UnidadTrabajo.Usuarios
                                .EncriptarPassword(model.Password);

                        }

                        modelUsuario.FehcaModificacion = DateTime.Now;
                        bandera = await _UnidadTrabajo.Usuarios.Actualizar(modelUsuario);


                        if (bandera)
                        {
                            mss = $"El Usuario :  {modelUsuario.UserName} ha sido Actualizado Correctamente";
                        }
                        else
                        {
                            mss = $"El Usuario :  {modelUsuario.UserName} no ha sido Actualizado.";
                        }

                        TempData[DS.Exitosa] = mss;
                    }
                    else
                    {
                        modelUsuario.Password = await _UnidadTrabajo.Usuarios
                              .EncriptarPassword(model.Password);

                        modelUsuario.FechaAlta = DateTime.Now;
                        modelUsuario.FehcaModificacion = DateTime.Now;
                        bandera = await _UnidadTrabajo.Usuarios.Agregar(modelUsuario);

                        if (bandera)
                        {
                            mss = $"El Usuario :  {modelUsuario.UserName} ha sido Agregado Correctamente";
                        }
                        else
                        {
                            mss = $"El Usuario :  {modelUsuario.UserName} no se Agrego.";
                        }

                        TempData[DS.Exitosa] = mss;
                    }

                    return Json(new { success = bandera });
                }
                else
                {
                    TempData[DS.Error] = "Error: " + string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                }


            }
            catch (Exception ex)
            {
                var mms = ex.InnerException.Message;
                TempData[DS.Error] = mms;
                return Json(new { success = false, message = ex.Message });
                throw new ArgumentNullException(ex.Message);

            }

            return Json("");
        }


        [HttpPost]

        public async Task<IActionResult> ActualizarPassWord([FromBody] PasswordDtos model)
        {
            Usuarios usuarioDb = new Usuarios();
            string mss = "";
            try
            {
                var usuario = await _UnidadTrabajo.Usuarios.GetObtener(model.Id);

                string passAntEncript = await _UnidadTrabajo.Usuarios.EncriptarPassword(model.PasswordAnterior);
                string passNewEncript = await _UnidadTrabajo.Usuarios.EncriptarPassword(model.Password);

                if (usuario.Password != passAntEncript) {
                    TempData[DS.Error] = "La Contraseña No Coincide, Favor de Validar.";
                    return Json(new { success = false }); 
                }

                if (ModelState.IsValid) {

                    usuarioDb = _mapper.Map<Usuarios>(model);
                    usuarioDb.Password = passNewEncript;

                    bool bandera = await _UnidadTrabajo.Usuarios.ActualizarPassword(usuarioDb);

                    if (bandera)
                    {
                        mss = $"La Contraseña ha sido Actualizada Correctamente";
                    }
                    else
                    {
                        mss = $"La Contraseña no se Actualizo.";
                    }

                    TempData[DS.Exitosa] = mss;
                    return Json(new { success = bandera });
                }
            }
            catch (Exception ex)
            {
                var mms = ex.InnerException.Message;
                TempData[DS.Error] = mms;
                return Json(new { success = false, message = ex.Message });
                throw new ArgumentNullException(ex.Message);

            }
            return Json("");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string mss = "";
            try
            {

                var objFromDb = await _UnidadTrabajo.Usuarios
                    .GetObtener(id);

                if (objFromDb != null)
                {
                    await _UnidadTrabajo.Usuarios.Eliminar(objFromDb);

                    mss = $"El Usuario :  {objFromDb.UserName} ha sido Eliminado Correctamente";
                    TempData[DS.Exitosa] = mss;
                    return Json(new { success = true, message = "Borrado Exitosamente" });
                }
                return Json(new { success = false, message = "Error al Borrar" });
            }
            catch (Exception ex)
            {
                TempData[DS.Error] = ex.Message;
                throw new ArgumentNullException(ex.Message);
            }
        }


        #region Api
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            try
            {
                var listaUsuarios = await _UnidadTrabajo.Usuarios
                    .GetObtenerTodos(x => x.UserName != "Admin", isTracking: false);

                return Json(new { data = listaUsuarios });
            }
            catch (Exception ex)
            {
                var mss = ex.InnerException.Message;
                throw new ArgumentNullException(ex.Message);
            }
        }

        [HttpGet]

        public async Task<JsonResult> llenarDashboard()
        {
            DatosUsuarios? cDatos = new DatosUsuarios();
            try
            {
                var lista = await _UnidadTrabajo.Usuarios.GetObtenerTodos();
                int activo = lista.Where(x => x.Estatus).ToList().Count();
                int inActivo = lista.Where(x => !x.Estatus).ToList().Count();
                int total = lista.Count();

                cDatos.Activos = activo;
                cDatos.InActivos = inActivo;
                cDatos.Total = total;

                return Json(new { data = cDatos });

            }
            catch (Exception ex)
            {
                var mss = ex.InnerException.Message;
                throw new ArgumentNullException(ex.Message);
            }
        }

        [HttpGet]

        public async Task<JsonResult> obtenerUsuario(int id)
        {
            try
            {
                var usuario = await _UnidadTrabajo
                    .Usuarios.GetObtener(id);

                return Json(new { data = usuario });
            }
            catch (Exception ex)
            {
                var mss = ex.InnerException.Message;
                throw new ArgumentNullException(ex.Message);
            }
        }


        #endregion
    }
}
