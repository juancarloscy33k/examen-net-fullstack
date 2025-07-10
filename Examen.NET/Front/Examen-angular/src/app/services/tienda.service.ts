import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Tienda {
  id: number;
  sucursal: string;
  direccion: string;
}

@Injectable({
  providedIn: 'root'
})
export class TiendaService {
  private apiUrl = 'https://localhost:44362/api/tienda';

  constructor(private http: HttpClient) {}

   getTiendas(): Observable<Tienda[]> {
    return this.http.get<Tienda[]>(this.apiUrl);
  }

  getTiendaById(id: number): Observable<Tienda> {
    return this.http.get<Tienda>(`${this.apiUrl}/${id}`);
  }

  createTienda(tienda: Tienda): Observable<any> {
    return this.http.post(this.apiUrl, tienda);
  }

  updateTienda(tienda: Tienda): Observable<any> {
    return this.http.put(this.apiUrl, tienda);
  }

  deleteTienda(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
