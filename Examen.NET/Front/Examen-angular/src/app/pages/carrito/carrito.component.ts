import { Component } from '@angular/core';
import { CarritoService } from '../../services/carrito.service';
import { AuthService } from '../../services/auth.service';
import { CompraService, PedidoArticulo } from '../../services/compra.service';
import { CommonModule } from '@angular/common';
import { Compra, CompraRequest } from '../../models/compra.model';

@Component({
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class CarritoComponent {
  carrito: PedidoArticulo[] = [];
  clienteId = 1;
  tiendaId = 1;

  constructor(
    private carritoService: CarritoService,
    private authService: AuthService,
    private compraService: CompraService
  ) {}

  ngOnInit() {
    const user = this.authService.getUser();
    if (user) this.clienteId = user.id;
    this.carrito = this.carritoService.obtenerArticulos();
  }

  comprar() {
    if (this.carrito.length === 0) {
      alert('El carrito está vacío');
      return;
    }

    const pedido: CompraRequest = {
      ClienteId: this.clienteId,
      TiendaId: this.tiendaId,
      Articulos: this.carrito.map(item => ({
        ArticuloId: item.articuloId,
        Cantidad: item.cantidad
      }))
    };

    this.compraService.realizarCompra(pedido).subscribe({
      next: () => {
        alert('Compra realizada correctamente ✅');
        this.carritoService.limpiarCarrito();
        this.carrito = [];
      },
      error: () => alert('Error al realizar compra ❌')
    });
  }
}