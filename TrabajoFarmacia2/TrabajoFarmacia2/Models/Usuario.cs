using System.ComponentModel.DataAnnotations;

namespace TrabajoFarmacia2.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contrasena es obligatoria")]
        public string Password { get; set; }
    }
}