export interface ClienteArticulo {
  clienteId: number;
  articuloId: number;
  fecha: string;
  cantidad: number;
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