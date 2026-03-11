using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.InterfazRepositorios
{
    public interface IUsuarios : IRepositorioGenerico<Usuarios>
    {
        Task<bool> Actualizar(Usuarios usuario);
        Task<string> EncriptarPassword(string pass);
        Task<bool> ActualizarPassword(Usuarios usuario);
    }
}
