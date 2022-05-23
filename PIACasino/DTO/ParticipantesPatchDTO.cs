using System.ComponentModel.DataAnnotations;

namespace PIACasino.DTO
{
    public class ParticipantesPatchDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string contraseña { get; set; }
    }
}
