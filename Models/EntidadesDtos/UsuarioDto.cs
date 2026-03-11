using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.EntidadesDtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La Nombre Completo es Requerido.")]
        [RegularExpression(@"^[^,:?""<>@{;.%¡!°¬=¿}#/\|]*$", ErrorMessage = "Caracter especial No Permitido.")]
        [MaxLength(200, ErrorMessage = "El Maximo de Caracteres Permitidos es de 200.")]
        public string? NombreCompleto { get; set; }

        [Required(ErrorMessage = "El Usuario es Requerido.")]
        [RegularExpression(@"^[^,:?""<>@{;.%¡!°¬=¿}#/\|]*$", ErrorMessage = "Caracter especial No Permitido.")]
        [MaxLength(20, ErrorMessage = "El Maximo de Caracteres Permitidos es de 20.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "La Contraseña es Requerida.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{14,}$",
       ErrorMessage = "El password debe tener mínimo 14 caracteres, una mayúscula, una minúscula, un número y un carácter especial.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "El Correo es Requerido.")]
        [EmailAddress(ErrorMessage = "El Formato del correo no es válido.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El Estatus del Usuario es Requerido.")]
        public bool Estatus { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FehcaModificacion { get; set; }
    }
}
