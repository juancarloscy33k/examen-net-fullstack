namespace Examen.NET.Entitys
{
    public class Models
    {
        public class Cliente
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Direccion { get; set; }
            public string Correo { get; set; }
            public string Passwor { get; set; }
            public ICollection<ClienteArticulo>? ClienteArticulos { get; set; }
        }

        public class Tienda
        {
            public int Id { get; set; }
            public string Sucursal { get; set; }
            public string Direccion { get; set; }
            public ICollection<ArticuloTienda>? ArticuloTienda { get; set; }
        }

        public class Articulo
        {
            public int Id { get; set; }
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
            public string Imagen { get; set; }
            public int Stock { get; set; }
            public ICollection<ArticuloTienda>? ArticuloTiendas { get; set; }
            public ICollection<ClienteArticulo>? ClienteArticulos { get; set; }

        }

        public class ArticuloTienda
        {
            public int ArticuloId { get; set; }
            public Articulo Articulo { get; set; }
            public int TiendaId { get; set; }
            public Tienda Tienda { get; set; }
            public DateTime Fecha { get; set; }
        }

        public class ClienteArticulo
        {
            public int Id { get; set; }     
            public int ClienteId { get; set; }
            public Cliente Cliente { get; set; }
            public int ArticuloId { get; set; }
            public Articulo Articulo { get; set; }
            public int Cantidad { get; set; }   
            public DateTime Fecha { get; set; }
            public Tienda Tienda { get; set; }
        }

        public class Login
        {
            public string Correo { get; set; }
            public string Passwor { get; set; }
        }

        public class PedidoArticulo
        {
            public int ArticuloId { get; set; }
            public int Cantidad { get; set; } 
        }

        public class Pedido
        {
            public int ClienteId { get; set; }
            public int TiendaId { get; set; }
            public List<PedidoArticulo> Articulos { get; set; }
        }
    }
}
