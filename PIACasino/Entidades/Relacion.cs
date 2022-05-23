
using System.ComponentModel.DataAnnotations;

namespace PIACasino.Entidades
{
    public class Relacion
    {
        public int NoLote { get; set; }
        public int ParticipanteId { get; set; }
        public int RifaId { get; set; } 
        public Rifa Rifa { get; set; }
        public Participante Participante { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NoLote < 1 || NoLote > 54)
            {
                yield return new ValidationResult("el nuemro debe estar dentro de 1 a 54", new string[]
                {
                    nameof(NoLote)
                });
            }
            throw new NotImplementedException();
        }


    }
}

