using System.ComponentModel.DataAnnotations;

namespace Lab17_A.Models
{
    public class PeliculaModel
    {
        public int IdPelicula { get; set; }

        [Required(ErrorMessage = "El campo Titulo es obligatorio")]
        public string? Titulo { get; set; }
        
        public string? Sipnosis { get; set; }

        [Required(ErrorMessage = "El campo Clasificación es obligatorio")]
        public string? Clasificacion { get; set; }

        [Required(ErrorMessage = "El campo Estudio de Filmación es obligatorio")]
        public string? EstudioFilmacion { get; set; }

        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Emisión es obligatorio")]
        public DateTime FechaEmision { get; set; }

        [Required(ErrorMessage = "El campo Idioma es obligatorio")]
        public string? Idioma { get; set; }

        [Required(ErrorMessage = "El campo Género es obligatorio")]
        public string? Genero { get; set; }

    }
}
