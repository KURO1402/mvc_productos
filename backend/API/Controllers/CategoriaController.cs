using DbModel.sistemaVentas;
using DtoModel.Categoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoriaController : ControllerBase
    {
        private readonly _sistemaVentasContext _db;

        public CategoriaController(_sistemaVentasContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDto>>> GetAll()
        {
            List<CategoriaDto> lista = await _db.Categoria
                .Select(c => new CategoriaDto
                {
                    IdCategoria = c.IdCategoria,
                    Nombre = c.Nombre
                })
                .ToListAsync();
            return Ok(lista);
        }
    }
}