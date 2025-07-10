import { Component } from '@angular/core';
import { TiendaService, Tienda } from '../../services/tienda.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../../shared/navbar/navbar.component';

@Component({
  selector: 'app-tiendas',
  imports: [CommonModule, FormsModule],
  templateUrl: './tiendas.component.html',
  styleUrl: './tiendas.component.css'
})
export class TiendasComponent {
  tiendas: Tienda[] = [];
  tiendaModel: Tienda = { id: 0, sucursal: '', direccion: '' };
  isEdit = false;

  constructor(private tiendaService: TiendaService) {
    this.loadTiendas();
  }

  loadTiendas() {
    this.tiendaService.getTiendas().subscribe(data => this.tiendas = data);
  }

  saveTienda() {
    if (this.isEdit) {
      this.tiendaService.updateTienda(this.tiendaModel).subscribe(() => {
        this.loadTiendas();
        this.resetForm();
      });
    } else {
      this.tiendaService.createTienda(this.tiendaModel).subscribe(() => {
        this.loadTiendas();
        this.resetForm();
      });
    }
  }

  editTienda(tienda: Tienda) {
    this.tiendaModel = { ...tienda };
    this.isEdit = true;
  }

  deleteTienda(id: number) {
    if (confirm('¿Deseas eliminar esta tienda?')) {
      this.tiendaService.deleteTienda(id).subscribe(() => {
        this.loadTiendas();
      });
    }
  }

  resetForm() {
    this.tiendaModel = { id: 0, sucursal: '', direccion: '' };
    this.isEdit = false;
  }
}