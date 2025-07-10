import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Articulo {
  id: number;
  codigo: string;
  descripcion: string;
  precio: number;
  imagen: string;
  stock: number;
}

@Injectable({
  providedIn: 'root'
})
export class ArticuloService {
  private apiUrl = 'https://localhost:44362/api/articulo';

  constructor(private http: HttpClient) {}

  getArticulos(): Observable<Articulo[]> {
    return this.http.get<Articulo[]>(this.apiUrl);
  }

  getArticuloById(id: number): Observable<Articulo> {
    return this.http.get<Articulo>(`${this.apiUrl}/${id}`);
  }

  createArticulo(articulo: Articulo): Observable<any> {
    return this.http.post(this.apiUrl, articulo);
  }

  updateArticulo(articulo: Articulo): Observable<any> {
    return this.http.put(this.apiUrl, articulo);
  }

  deleteArticulo(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
