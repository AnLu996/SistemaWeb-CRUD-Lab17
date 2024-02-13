using System.ComponentModel.DataAnnotations;

namespace Lab17_A.Models
{
    public class VacunaModel
    {
        public int IdVacuna { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string? Nombre { get; set; }
        public DateTime FechaFabricacion { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Vencimiento es obligatorio")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "El campo Laboratorio es obligatorio")]
        public string? Laboratorio { get; set; }

    }
}
