using Examen.NET.Bussiness;
using Microsoft.AspNetCore.Mvc;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Controllers
{
    [ApiController]
    [Route("api/compra")]
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet("all")]
        public IActionResult ObtenerTodasLasCompras()
        {
            var compras = _compraService.getAllCompras();
            return Ok(compras);
        }

        [HttpGet("cliente/{clienteId}")]
        public IActionResult ObtenerComprasPorCliente(int clienteId)
        {
            var compras = _compraService.getComprasCliente(clienteId);
            return Ok(compras);
        }

        [HttpGet("tienda/{tiendaId}")]
        public IActionResult ObtenerComprasPorTienda(int tiendaId)
        {
            var compras = _compraService.getComprasTienda(tiendaId);
            return Ok(compras);
        }

        [HttpGet("todas")]
        public IActionResult GetTodasLasCompras()
        {
            var compras = _compraService.getAllCompras();
            return Ok(compras);
        }

        [HttpPost("agregar")]
        public IActionResult AgregarCompra([FromBody] ClienteArticulo compra)
        {
            _compraService.createCompra(compra);
            return Ok(new { message = "Compra registrada correctamente" });
        }

        [HttpPost]
        public IActionResult RealizarCompra([FromBody] Pedido pedido)
        {
            if (pedido == null || pedido.Articulos == null || !pedido.Articulos.Any())
                return BadRequest("Pedido inválido.");

            if (!_compraService.ClienteExiste(pedido.ClienteId))
                return BadRequest("Cliente no existe.");

            foreach (var articulo in pedido.Articulos)
            {
                if (!_compraService.ArticuloExiste(articulo.ArticuloId))
                    return BadRequest($"Artículo con ID {articulo.ArticuloId} no existe.");

                var compra = new ClienteArticulo
                {
                    ClienteId = pedido.ClienteId,
                    ArticuloId = articulo.ArticuloId,
                    Cantidad = articulo.Cantidad,
                    Fecha = DateTime.Now
                };

                _compraService.createCompra(compra);
            }

            return Ok(new { message = "Compra realizada correctamente" });
        }

        [HttpDelete]
        public IActionResult EliminarCompra([FromQuery] int ClienteId, [FromQuery] int ArticuloId)
        {
            _compraService.deleteCompra(ClienteId, ArticuloId);
            return Ok(new { message = "Compra eliminada" });
        }
    }
}
