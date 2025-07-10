import { Injectable } from '@angular/core';
import { Articulo } from './articulo.service';
import { PedidoArticulo } from '../models/pedido.model';

@Injectable({
  providedIn: 'root'
})
export class CarritoService {
   private carrito: PedidoArticulo[] = [];

  agregarArticulo(articulo: Articulo) {
    const existente = this.carrito.find(p => p.articuloId === articulo.id);
    if (existente) {
      existente.cantidad += 1;
    } else {
      this.carrito.push({
        articuloId: articulo.id,
        cantidad: 1,
        articulo: articulo
      });
    }
  }

  limpiarCarrito() {
    this.carrito = [];
  }

  obtenerArticulos(): PedidoArticulo[] {
    return this.carrito;
  }
}