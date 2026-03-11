using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entidades
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Correo { get; set; }
        public bool Estatus {  get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FehcaModificacion { get; set; } 

    }
}
