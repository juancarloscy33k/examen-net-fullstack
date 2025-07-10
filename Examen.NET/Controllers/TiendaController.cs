using Examen.NET.Bussiness;
using Microsoft.AspNetCore.Mvc;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Controllers
{
    [ApiController]
    [Route("api/tienda")]
    public class TiendaController : ControllerBase
    {
        private readonly ITiendaService _tiendaService;

        public TiendaController(ITiendaService tiendaService)
        {
            _tiendaService = tiendaService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_tiendaService.allTiendas());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tienda = _tiendaService.getTiendaId(id);
            if (tienda == null) return NotFound();
            return Ok(tienda);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tienda tienda)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _tiendaService.createTienda(tienda);
            return Ok(new { message = "Tienda creada correctamente" });
        }

        [HttpPut]
        public IActionResult Update([FromBody] Tienda tienda)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = _tiendaService.getTiendaId(tienda.Id);
            if (existing == null) return NotFound();

            _tiendaService.updateTienda(tienda);
            return Ok(new { message = "Tienda actualizada" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _tiendaService.getTiendaId(id);
            if (existing == null) return NotFound();

            _tiendaService.deleteTienda(id);
            return Ok(new { message = "Tienda eliminada" });
        }
    }
}
