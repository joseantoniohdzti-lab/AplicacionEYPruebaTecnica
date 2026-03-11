using AccesoDatos.Data;
using AccesoDatos.InterfazRepositorios;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace AccesoDatos.Repositorios
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuarios>, IUsuarios
    {
        private readonly AppDbContext _db;
        public UsuarioRepositorio(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> Actualizar(Usuarios usuario)
        {
            try
            {
                var usuarioDb = await _db.Usuarios
                    .FirstOrDefaultAsync(x => x.Id == usuario.Id);

                if (usuarioDb != null)
                {
                    usuarioDb.UserName = usuario.UserName;
                    usuarioDb.NombreCompleto = usuario.NombreCompleto;
                    usuarioDb.FehcaModificacion = usuario.FehcaModificacion;
                    usuarioDb.Correo = usuario.Correo;
                    usuarioDb.Estatus = usuario.Estatus;
                    usuarioDb.Password = usuario.Password;

                }

                return await _db.SaveChangesAsync() > 0 ? true : false;

            }
            catch (Exception ex)
            {
                var mss = ex.InnerException.Message;
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<bool> ActualizarPassword(Usuarios usuario)
        {
            try
            {
                var usuarioDb = await _db.Usuarios
                    .FirstOrDefaultAsync(x => x.Id == usuario.Id);

                if (usuarioDb != null)
                {
                    usuarioDb.Password = usuario.Password;

                }

                return await _db.SaveChangesAsync() > 0 ? true : false;

            }
            catch (Exception ex)
            {
                var mss = ex.InnerException.Message;
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<string> EncriptarPassword(string pass)
        {
            using var md5 = MD5.Create();
            byte[] data = Encoding.UTF8.GetBytes(pass);
            data =  md5.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
            {
                resp += data[i].ToString("x2").ToLower();
            }

            return resp;
        }
    }
}
