import { Component } from '@angular/core';
import { ArticuloService, Articulo } from '../../services/articulo.service';
import { AuthService } from '../../services/auth.service';
import { CarritoService } from '../../services/carrito.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../../shared/navbar/navbar.component';

@Component({
  selector: 'app-articulos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './articulos.component.html',
  styleUrl: './articulos.component.css'
})
export class ArticulosComponent {
  articulos: Articulo[] = [];
  isAdmin: boolean = false;

  articuloSeleccionado: Articulo | null = null;
  esEdicion: boolean = false;

  constructor(
    private articuloService: ArticuloService,
    private authService: AuthService,
    private carritoService: CarritoService
  ) {
    this.isAdmin = this.authService.isAdmin();
    this.loadArticulos();
  }

  ngOnInit() {
    this.isAdmin = this.authService.isAdmin();
  this.loadArticulos();
  }
  loadArticulos() {
    this.articuloService.getArticulos().subscribe(data => this.articulos = data);
  }

  nuevoArticulo() {
    this.articuloSeleccionado = {
      id: 0,
      codigo: '',
      descripcion: '',
      precio: 0,
      imagen: '',
      stock: 0
    };
    this.esEdicion = false;
  }

  editarArticulo(articulo: Articulo) {
    this.articuloSeleccionado = { ...articulo }; 
    this.esEdicion = true;
  }

  guardarArticulo() {
    if (!this.articuloSeleccionado) return;

    if (this.esEdicion) {
      this.articuloService.updateArticulo(this.articuloSeleccionado).subscribe(() => {
        alert('Artículo actualizado');
        this.loadArticulos();
        this.articuloSeleccionado = null;
      });
    } else {
      this.articuloService.createArticulo(this.articuloSeleccionado).subscribe(() => {
        alert('Artículo creado');
        this.loadArticulos();
        this.articuloSeleccionado = null;
      });
    }
  }

  cancelar() {
    this.articuloSeleccionado = null;
  }

  eliminarArticulo(id: number) {
    if (!confirm('¿Estás seguro de eliminar este artículo?')) return;
    this.articuloService.deleteArticulo(id).subscribe(() => {
      alert('Artículo eliminado');
      this.loadArticulos();
    });
  }

  agregarAlCarrito(articulo: Articulo) {
    this.carritoService.agregarArticulo(articulo);
    alert(`"${articulo.descripcion}" agregado al carrito`);
  }
}