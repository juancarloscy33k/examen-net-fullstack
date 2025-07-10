using Examen.NET.Data;
using static Examen.NET.Entitys.Models;

namespace Examen.NET.Bussiness
{
    public class ArticuloService : IArticuloService
    {
        private readonly ArticulosData _articulosData;

        public ArticuloService(ArticulosData articulosData)
        {
            _articulosData = articulosData;
        }

        public List<Articulo> allArticulos() => _articulosData.allArticulos();

        public Articulo getArticuloId(int id) => _articulosData.getArticuloId(id);

        public void createArticulo(Articulo articulo) => _articulosData.createArticulo (articulo);

        public void updateArticulo(Articulo articulo) => _articulosData.updateArticulo(articulo);

        public void deleteArticulo(int id) => _articulosData.deleteArticulo(id);
    }
}
