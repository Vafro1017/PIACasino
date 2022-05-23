using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIACasino.DTO;
using PIACasino.Entidades;

namespace PIACasino.Controllers
{
    [ApiController]
    [Route("api/rifas/{rifaId:int}/premios")]
    public class PremioController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public PremioController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;  
        }

        [HttpGet]
        public async Task<ActionResult<List<PremioDTO>>> Get()
        {
            var premio = await dbContext.Premios.ToListAsync();
            return mapper.Map<List<PremioDTO>>(premio);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PremioCreacionDTO premioCreacionDTO)
        {
            var rifaid = premioCreacionDTO.RifaId;

            var rifaBd = await dbContext.Rifas.Include(rifa => rifa.Premios)
                .FirstOrDefaultAsync(x => x.Id == rifaid);
            if (rifaBd == null)
            {
                return BadRequest();
            }

            var premio = mapper.Map<Premio>(premioCreacionDTO);

            premio.Rifa = rifaBd;

            dbContext.Add(premio);
            await dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbContext.Premios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("no existe dicho premio");
            }

            dbContext.Remove(new Premio()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
