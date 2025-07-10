export interface Compra {
  clienteId: number;
  articuloId: number;
  tiendaId: number;
  cantidad: number;
  fecha: Date; 
}
export interface CompraRequest {
  ClienteId: number;
  TiendaId: number;
  Articulos: {
    ArticuloId: number;
    Cantidad: number;
  }[];
}
