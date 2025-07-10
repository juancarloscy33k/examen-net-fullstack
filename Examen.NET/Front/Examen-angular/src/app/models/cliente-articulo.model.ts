export interface ClienteArticulo {
  clienteId: number;
  articuloId: number;
  fecha: string;
  cantidad: number;
  tienda?: {
    id: number;
    sucursal: string;
  };
  cliente?: {
    nombre: string;
    apellido: string;
  };
  articulo?: {
    descripcion: string;
    codigo: string;
    precio: number;
  };
}