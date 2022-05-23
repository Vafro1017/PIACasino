using System.ComponentModel.DataAnnotations;

namespace PIACasino.DTO
{
    public class ParticipanteCreacionDTO
    {
        
        [Required]
        public string Email { get; set; }
        [Required]
        public string contraseña { get; set; }
    }
}
