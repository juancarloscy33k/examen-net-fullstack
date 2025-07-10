using static Examen.NET.Entitys.Models;

namespace Examen.NET.Bussiness
{
    public interface ICompraService
    {
        List<ClienteArticulo> getAllCompras();
        List<ClienteArticulo> getComprasTienda(int tiendaId);
        void createCompra(ClienteArticulo compra);
        List<ClienteArticulo> getComprasCliente(int ClienteId);
        void deleteCompra(int ClienteId, int ArticuloId);
        bool ClienteExiste(int clienteId);
        bool ArticuloExiste(int articuloId);
    }
}
