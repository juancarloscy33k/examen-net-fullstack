using Examen.NET.Data;
using Microsoft.EntityFrameworkCore;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Bussiness
{
    public class CompraService : ICompraService
    {
        private readonly CompraData _compraData;

        public CompraService(CompraData compraData)
        {
            _compraData = compraData;
        }
        public List<ClienteArticulo> getAllCompras() => _compraData.getAllCompras();
        public List<ClienteArticulo> getComprasTienda(int tiendaId) => _compraData.getComprasTienda(tiendaId);
        public void createCompra(ClienteArticulo compra) => _compraData.createCompra(compra);

        public List<ClienteArticulo> getComprasCliente(int ClienteId) => _compraData.getComprasCliente(ClienteId);

        public void deleteCompra(int ClienteId, int ArticuloId) => _compraData.deleteCompra(ClienteId, ArticuloId);
        public bool ClienteExiste(int clienteId)
        {
            return _compraData.ClienteExiste(clienteId);
        }

        public bool ArticuloExiste(int articuloId)
        {
            return _compraData.ArticuloExiste(articuloId);
        }
    }
}
