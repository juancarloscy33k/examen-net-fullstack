using Examen.NET.Data;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Bussiness
{
    public class ClienteService : IClienteService
    {
        private readonly ClienteData _clienteData;

        public ClienteService(ClienteData clienteData)
        {
            _clienteData = clienteData;
        }

        public List<Cliente> Clientes() => _clienteData.Clientes();
        public Cliente GetClientId(int Id) => _clienteData.GetClientId(Id);
        public void createClient(Cliente cliente) => _clienteData.createClient(cliente);
        public void updateClient(Cliente cliente) => _clienteData.updateClient(cliente);
        public void deleteClient(int Id) => _clienteData.deleteClient(Id);
        public Cliente? login(string correo, string passwor)
        {
            return _clienteData.Login(correo, passwor);
        }
    }
}
