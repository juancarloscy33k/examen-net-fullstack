using static Examen.NET.Entitys.Models;

namespace Examen.NET.Bussiness
{
    public interface IClienteService
    {
        List<Cliente> Clientes();
        Cliente GetClientId(int Id);
        void createClient(Cliente cliente);
        void updateClient(Cliente cliente);
        void deleteClient(int Id);
        Cliente? login(string correo, string passwor);
    }
}
