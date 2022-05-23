using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PIACasino.Entidades;
using PIACasino.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.JsonPatch;

namespace PIACasino.Controller
{
    [ApiController]
    [Route("api/participantes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ParticipanteController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public ParticipanteController(ApplicationDbContext context,IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ParticipanteDTO>> Get()
        {
            var p = await dbContext.Participantes.ToListAsync();
            return mapper.Map<List<ParticipanteDTO>>(p);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ParticipanteDTO>> Get(int id)
        {
            var p = await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<ParticipanteDTO>(p);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] ParticipanteCreacionDTO participanteCreacionDTO)
        {
        
            var participante = mapper.Map<Participante>(participanteCreacionDTO);
            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<ParticipantesPatchDTO>patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var participanteBD = await dbContext.Participantes.FirstOrDefaultAsync(x=> x.Id == id);
            if (participanteBD == null)
            {
                return BadRequest();
            }

            var participanteDTO = mapper.Map<ParticipantesPatchDTO>(participanteBD);
            patchDocument.ApplyTo(participanteDTO,ModelState);
            var validez = TryValidateModel(participanteDTO);

            if (!validez)
            {
                return BadRequest();
            }

            mapper.Map(participanteDTO, participanteBD);

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

            dbContext.Remove(new Participante()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
