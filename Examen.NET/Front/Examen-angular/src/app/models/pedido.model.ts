import { Articulo } from './articulo.model';

export interface PedidoArticulo {
  articuloId: number;
  cantidad: number;
  articulo?: Articulo;
}

export interface Pedido {
  clienteId: number;
  tiendaId: number;
  articulos: PedidoArticulo[];
}