import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ClienteArticulo } from '../models/cliente-articulo.model';

export interface Cliente {
  id: number;
  nombre: string;
  apellido: string;
  direccion: string;
  correo: string;
  passwor: string;
}

export interface ClienteNuevo {
  nombre: string;
  apellido: string;
  direccion: string;
  correo: string;
  passwor: string;
}

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
   private apiUrl = 'https://localhost:44362/api/clientes';

  constructor(private http: HttpClient) { }

  getClientes(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(this.apiUrl);
  }

  obtenerComprasPorCliente(clienteId: number): Observable<ClienteArticulo[]> {
    return this.http.get<ClienteArticulo[]>(`${this.apiUrl}/cliente/${clienteId}`);
  }

  getCliente(id: number): Observable<Cliente> {
    return this.http.get<Cliente>(`${this.apiUrl}/${id}`);
  }

  createCliente(cliente: ClienteNuevo): Observable<any> {
    return this.http.post(this.apiUrl, cliente);
  }

  updateCliente(cliente: Cliente): Observable<any> {
    return this.http.put(this.apiUrl, cliente);
  }

  deleteCliente(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  login(correo: string, passwor: string): Observable<Cliente | null> {
    return this.http.post<Cliente | null>(`${this.apiUrl}/login`, { correo, passwor });
  }
}
