using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.EntidadesDtos
{
    public class PasswordDtos
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "La Contraseña Nueva es Requerida.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{14,}$",
       ErrorMessage = "El password debe tener mínimo 14 caracteres, una mayúscula, una minúscula, un número y un carácter especial.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "La Contraseña Anterior es Requerida.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{14,}$",
       ErrorMessage = "El password debe tener mínimo 14 caracteres, una mayúscula, una minúscula, un número y un carácter especial.")]
        public string? PasswordAnterior { get; set; }
    }
}
