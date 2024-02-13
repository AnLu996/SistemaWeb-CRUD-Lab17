using System.ComponentModel.DataAnnotations;

namespace Lab17_A.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        public string? perfil { get; set; }

        [Required(ErrorMessage = "Ingrese su contraseña")]
        public string? contraseña { get; set; }


        [Required(ErrorMessage = "Debes confirmar la contraseña")]
        public string? ConfirmarContraseña { get; set; }
    }
}
