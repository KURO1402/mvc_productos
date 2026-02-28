using DtoModel.Producto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Business.Producto;

namespace SistemaVentas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoBusiness _productoBusiness;

        public ProductoController(IProductoBusiness productoBusiness)
        {
            _productoBusiness = productoBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDto>>> GetAll()
        {
            List<ProductoDto> list = await _productoBusiness.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetById(int id)
        {
            ProductoDto? producto = await _productoBusiness.GetById(id);
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Create([FromBody] ProductoDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProductoDto producto = await _productoBusiness.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = producto.IdProducto }, producto);
        }

        [HttpPut]
        public async Task<ActionResult<ProductoDto>> Update([FromBody] ProductoDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProductoDto? producto = await _productoBusiness.Update(request);
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }
            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            ProductoDto? producto = await _productoBusiness.GetById(id);
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }
            await _productoBusiness.Delete(id);
            return NoContent();
        }
    }
}