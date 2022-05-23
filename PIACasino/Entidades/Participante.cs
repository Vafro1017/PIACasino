using System.ComponentModel.DataAnnotations;

namespace PIACasino.Entidades
{
    public class Participante
    {

        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string contraseña { get; set; }
        public List<Relacion> Relaciones { get; }
    }
}
