using Examen.NET.Bussiness;
using Microsoft.AspNetCore.Mvc;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Controllers
{
    [ApiController]
    [Route("api/articulo")]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _articuloService;

        public ArticuloController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_articuloService.allArticulos());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var articulo = _articuloService.getArticuloId(id);
            if (articulo == null) return NotFound();
            return Ok(articulo);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Articulo articulo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _articuloService.createArticulo(articulo);
            return Ok(new { message = "Artículo creado correctamente" });
        }

        [HttpPut]
        public IActionResult Update([FromBody] Articulo articulo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = _articuloService.getArticuloId(articulo.Id);
            if (existing == null) return NotFound();

            _articuloService.updateArticulo(articulo);
            return Ok(new { message = "Artículo actualizado" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _articuloService.getArticuloId(id);
            if (existing == null) return NotFound();

            _articuloService.deleteArticulo(id);
            return Ok(new { message = "Artículo eliminado" });
        }
    }
}
