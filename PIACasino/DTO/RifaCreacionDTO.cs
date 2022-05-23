using System.ComponentModel.DataAnnotations;

namespace PIACasino.DTO
{
    public class RifaCreacionDTO
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string Nombre { get; set; }
        public List<int> ParticipantesIds { get; set; }
    }
}
