using System.ComponentModel.DataAnnotations;

namespace PIACasino.DTO
{
    public class ModificarRifa
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Nombre { get; set; }
    }
}
