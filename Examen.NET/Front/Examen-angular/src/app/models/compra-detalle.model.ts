export interface CompraConDetalle {
  clienteNombre: string;
  clienteApellido: string;
  articuloDescripcion: string;
  articuloCodigo: string;
  articuloPrecio: number;
  cantidad: number;
  fecha: Date;
  tiendaSucursal: string;
}