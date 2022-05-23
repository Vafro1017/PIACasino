using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIACasino.DTO;
using PIACasino.Entidades;

namespace PIACasino.Controllers
{
   [ApiController]
   [Route("api/rifas")]
    public class RifaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public RifaController(ApplicationDbContext context,IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<RifaDTO>> Get()
        {
            var rifa = await dbContext.Rifas.ToListAsync();
            return mapper.Map<List<RifaDTO>>(rifa);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RifaDTO>> Get(int id)
        {
            var rifa = await dbContext.Rifas.
                Include(rifaBD => rifaBD.Relaciones)
                .ThenInclude(relacionBD => relacionBD.Participante)
                .FirstOrDefaultAsync(rifaBD=> rifaBD.Id == id);
            
            if (rifa == null)
            {
                return NotFound();
            }
            
            return mapper.Map<RifaDTO>(rifa);
        }


        [HttpGet("{nombre}")]
        public async Task<ActionResult<RifaDTO>> Get(string nombre)
        {
            var rifa = await dbContext.Rifas.FirstOrDefaultAsync(rifaBD => rifaBD.Nombre == nombre);

            return mapper.Map<RifaDTO>(rifa);
        }


        [HttpPost]
        public async Task<ActionResult> Post(RifaCreacionDTO rifaCreacionDTO)
        {
           
            var participantesIds = await dbContext.Participantes
                .Where(rifaBD => rifaCreacionDTO.
                ParticipantesIds.Contains(rifaBD.Id)).Select(x => x.Id).ToListAsync();

            if(rifaCreacionDTO.ParticipantesIds.Count != participantesIds.Count)
            {
                return BadRequest("Uno de los participantes no existe");
            }
            var r = mapper.Map<Rifa>(rifaCreacionDTO);

            /*foreach(var p in rifaCreacionDTO.ParticipantesIds)
            {
                var part = await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == p);
                r.Relaciones.Add( new Relacion() { ParticipanteId = p, Participante = part });
            }*/

            dbContext.Add(r);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
       
        [HttpPut("{id:int}")] 
        public async Task<ActionResult> Put(ModificarRifa modificarRifa, int id)
        {

            var rifa = mapper.Map<Rifa>(modificarRifa);
            rifa.Id = id;

            dbContext.Update(rifa);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Rifa()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
