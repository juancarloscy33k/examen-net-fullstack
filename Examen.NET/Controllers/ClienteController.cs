using Examen.NET.Bussiness;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var clientes = _clienteService.Clientes();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int Id)
        {
            var cliente = _clienteService.GetClientId(Id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cliente cliente)
        {
            _clienteService.createClient(cliente);
            return Ok(new { message = "Cliente creado correctamente" });
        }

        [HttpPut]
        public IActionResult Update([FromBody] Cliente cliente)
        {
            _clienteService.updateClient(cliente);
            return Ok(new { message = "Cliente actualizado" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            _clienteService.deleteClient(Id);
            return Ok(new { message = "Cliente eliminado" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var result = _clienteService.login(login.Correo, login.Passwor);
            if (result == null)
                return Unauthorized("Correo o contraseña incorrectos");

            return Ok(result);
        }
    }
}
