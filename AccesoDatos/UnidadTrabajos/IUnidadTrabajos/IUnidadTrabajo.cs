using AccesoDatos.InterfazRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.UnidadTrabajos.IUnidadTrabajos
{
    public interface IUnidadTrabajo
    {
        public IUsuarios Usuarios { get; }
        Task Guardar();
    }
}
