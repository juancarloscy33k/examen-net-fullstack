import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HostListener } from '@angular/core';
import { Tienda, TiendaService } from '../../services/tienda.service';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { Cliente, ClienteService } from '../../services/cliente.service';
import { Articulo, ArticuloService } from '../../services/articulo.service';
import {CompraService } from '../../services/compra.service';
import { ClienteArticulo } from '../../models/cliente-articulo.model';

@Component({
  selector: 'app-compra',  
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './compra.component.html',
  styleUrl: './compra.component.css'
})
export class CompraComponent {
  clienteId: number | null = null;
  isAdmin = false;

  comprasConDetalle: any[] = [];

  clientes: Cliente[] = [];
  articulos: Articulo[] = [];
  tiendas: Tienda[] = [];

  filtro: string = '';
  compras: any[] = [];

  ngOnDestroy() {
    this.limpiarDatos();
  }

  @HostListener('window:beforeunload', ['$event'])
  unloadHandler(event: Event) {
    this.limpiarDatos();
  }

  limpiarDatos() {
    this.compras = [];
  }
  constructor(
    private clienteService: ClienteService,
    private articuloService: ArticuloService,
    private tiendaService: TiendaService,
    private compraService: CompraService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    const user = this.authService.getUser();
    this.clienteId = user?.id || null;
    this.isAdmin = user?.correo?.toLowerCase() === 'admin@admin.com';

    this.cargarDatosYCompras();
  }

  mapearComprasConDetalle(compras: ClienteArticulo[]) {
    this.comprasConDetalle = compras.map(compra => {
      const cliente = this.clientes.find(c => c.id === compra.clienteId);
      const articulo = this.articulos.find(a => a.id === compra.articuloId);
      const tiendaSucursal = compra.tienda?.sucursal || 'N/A';

      return {
        clienteNombre: cliente?.nombre || 'N/A',
        clienteApellido: cliente?.apellido || '',
        articuloDescripcion: articulo?.descripcion || 'N/A',
        articuloCodigo: articulo?.codigo || '',
        articuloPrecio: articulo?.precio || 0,
        cantidad: compra.cantidad,
        fecha: compra.fecha,
        tiendaSucursal: tiendaSucursal
      };
    });
  }

  cargarDatosYCompras() {
    this.clienteService.getClientes().subscribe(clientes => {
      this.clientes = clientes;

      this.articuloService.getArticulos().subscribe(articulos => {
        this.articulos = articulos;

        this.tiendaService.getTiendas().subscribe(tiendas => {
          this.tiendas = tiendas;

          if (this.isAdmin) {
            this.compraService.obtenerTodasLasCompras().subscribe((compras: ClienteArticulo[]) => {
              this.mapearComprasConDetalle(compras);
            });
          } else if (this.clienteId) {
            this.compraService.obtenerComprasPorCliente(this.clienteId).subscribe((compras: ClienteArticulo[]) => {
              this.mapearComprasConDetalle(compras);
            });
          }
        });
      });
    });
  }

  get comprasFiltradas() {
    if (!this.filtro) return this.comprasConDetalle;

    const term = this.filtro.toLowerCase();
    return this.comprasConDetalle.filter(c =>
      `${c.clienteNombre} ${c.clienteApellido}`.toLowerCase().includes(term) ||
      c.articuloDescripcion.toLowerCase().includes(term) ||
      c.articuloCodigo.toLowerCase().includes(term) ||
      c.tiendaSucursal.toLowerCase().includes(term)
    );
  }
}