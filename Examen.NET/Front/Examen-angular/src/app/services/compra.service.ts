import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { ClienteArticulo } from '../models/cliente-articulo.model';
import { Articulo } from './articulo.service';

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

@Injectable({
  providedIn: 'root'
})
export class CompraService {
  private apiUrl = 'https://localhost:44362/api/compra';

  constructor(private http: HttpClient) {}

  obtenerComprasPorCliente(clienteId: number): Observable<ClienteArticulo[]> {
    return this.http.get<ClienteArticulo[]>(`${this.apiUrl}/cliente/${clienteId}`);
  }

  obtenerTodasLasCompras(): Observable<ClienteArticulo[]> {
    return this.http.get<ClienteArticulo[]>(`${this.apiUrl}/all`);
  }

  realizarCompra(pedido: any): Observable<any> {
    return this.http.post(this.apiUrl, pedido);
  }

  obtenerComprasPorTienda(tiendaId: number): Observable<ClienteArticulo[]> {
    return this.http.get<ClienteArticulo[]>(`${this.apiUrl}/tienda/${tiendaId}`);
  }
}