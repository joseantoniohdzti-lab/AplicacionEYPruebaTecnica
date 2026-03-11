using System;
using System.Collections.Generic;
using System.Text;

namespace Models.EntidadesDtos
{
    public class AddUsuariosDto
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Correo { get; set; }
        public bool Estatus { get; set; }
    }
}
