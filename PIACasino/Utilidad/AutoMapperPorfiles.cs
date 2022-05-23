using AutoMapper;
using PIACasino.DTO;
using PIACasino.Entidades;

namespace PIACasino.Utilidad
{
    public class AutoMapperPorfiles : Profile
    {
        public AutoMapperPorfiles()
        {
            CreateMap<RifaCreacionDTO, Rifa>()
                .ForMember(rifa => rifa.Relaciones, op => op.MapFrom(MapRelaciones));

            //CreateMap<RifaCreacionDTO, Rifa>();

            CreateMap<Rifa, RifaDTO>()
                .ForMember(rifaDTO => rifaDTO.Participantes, op => op.MapFrom(MapRifaDTOParticipantes));
            CreateMap<ParticipanteCreacionDTO, Participante>();
            CreateMap<Participante, ParticipanteDTO>();
            CreateMap<ModificarRifa, Rifa>();
            CreateMap<Participante,ParticipantesPatchDTO>().ReverseMap();
            CreateMap<PremioCreacionDTO, Premio>();
            CreateMap<Premio, PremioDTO>();
        }
        
        private List<ParticipanteDTO> MapRifaDTOParticipantes(Rifa rifa, RifaDTO rifaDTO)
        {
            var r = new List<ParticipanteDTO>();
            if (rifa.Relaciones == null)
            {
                return r; 
            }

            foreach (var relaciones in rifa.Relaciones)
            {
                r.Add(new ParticipanteDTO() {
                Id = relaciones.ParticipanteId,
                Email= relaciones.Participante.Email
                });
            }
            return r;
        }

        private List<Relacion> MapRelaciones(RifaCreacionDTO rifaCreacionDTO, Rifa rifa)
        {
            var r = new List<Relacion>();
            if(rifaCreacionDTO.ParticipantesIds == null )
            {
                return r;
            }
            foreach (var participanteId in rifaCreacionDTO.ParticipantesIds)
            {
                r.Add(new Relacion() { ParticipanteId = participanteId});            
            }

            return r; 
        }
    }
}
