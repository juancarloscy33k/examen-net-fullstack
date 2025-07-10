using System.Collections.Generic;
using Examen.NET.Data;
using static Examen.NET.Entitys.Models;
namespace Examen.NET.Bussiness
{
    public class TiendaService : ITiendaService
    {
        private readonly TiendaData _tiendaData;

        public TiendaService(TiendaData tiendaData)
        {
            _tiendaData = tiendaData;
        }

        public List<Tienda> allTiendas() => _tiendaData.allTiendas();
        public Tienda getTiendaId(int Id) => _tiendaData.getTiendaId(Id);
        public void createTienda(Tienda tienda) => _tiendaData.createTienda(tienda);
        public void updateTienda(Tienda tienda) => _tiendaData.updateTienda(tienda);
        public void deleteTienda(int Id) => _tiendaData.deleteTienda(Id);
    }
}
