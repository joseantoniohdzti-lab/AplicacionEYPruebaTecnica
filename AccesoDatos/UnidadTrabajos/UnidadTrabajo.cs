using AccesoDatos.Data;
using AccesoDatos.InterfazRepositorios;
using AccesoDatos.Repositorios;
using AccesoDatos.UnidadTrabajos.IUnidadTrabajos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.UnidadTrabajos
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly AppDbContext _db;

        public IUsuarios Usuarios { get; private set; }


        public UnidadTrabajo(AppDbContext db)
        {
            _db = db;
            Usuarios = new UsuarioRepositorio(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
