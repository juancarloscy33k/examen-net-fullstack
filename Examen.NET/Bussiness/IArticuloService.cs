using static Examen.NET.Entitys.Models;

namespace Examen.NET.Bussiness
{
    public interface IArticuloService
    {
        List<Articulo> allArticulos();
        Articulo getArticuloId(int Id);
        void createArticulo(Articulo articulo);
        void updateArticulo(Articulo articulo);
        void deleteArticulo(int Id);
    }
}
