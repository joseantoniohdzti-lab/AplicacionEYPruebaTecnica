using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El Usuario es requerido")]
        [Display(Name = "Usuario")]
        [RegularExpression(@"^[^,:?""<>@{;.%¡!°¬=¿}#/\|]*$", ErrorMessage = "Caracter especial No Permitido.")]
        [MaxLength(20, ErrorMessage = "El Maximo de Caracteres Permitidos es de 20.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
