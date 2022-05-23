using System.ComponentModel.DataAnnotations;

namespace PIACasino.Entidades
{
    public class Rifa
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Nombre { get; set; }
        public List<Relacion> Relaciones { get; set; }
        public List<Premio> Premios { get; set; }
       
    }
}
