using static Examen.NET.Entitys.Models;

namespace Examen.NET.Bussiness
{
    public interface ITiendaService
    {
        List<Tienda> allTiendas();
        Tienda getTiendaId(int Id);
        void createTienda(Tienda tienda);
        void updateTienda(Tienda tienda);
        void deleteTienda(int Id);

    }
}
