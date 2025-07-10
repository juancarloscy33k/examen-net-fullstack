import { Component } from '@angular/core';
import { ClienteService, Cliente } from '../../services/cliente.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../../shared/navbar/navbar.component';

@Component({
  selector: 'app-clientes',  
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './clientes.component.html',
  styleUrl: './clientes.component.css'
})
export class ClientesComponent {
clientes: Cliente[] = [];
  clienteModel: Cliente = { id: 0, nombre: '', apellido: '', direccion: '', correo: '', passwor: '' };
  isEdit = false;

  constructor(private clienteService: ClienteService) {
    this.loadClientes();
  }

  loadClientes() {
    this.clienteService.getClientes().subscribe(data => this.clientes = data);
  }

  saveCliente() {
    if (this.isEdit) {
      this.clienteService.updateCliente(this.clienteModel).subscribe(() => {
        this.loadClientes();
        this.resetForm();
      });
    } else {
      this.clienteService.createCliente(this.clienteModel).subscribe(() => {
        this.loadClientes();
        this.resetForm();
      });
    }
  }

  editCliente(cliente: Cliente) {
    this.clienteModel = { ...cliente };
    this.isEdit = true;
  }

  deleteCliente(id: number) {
    if (confirm('¿Deseas eliminar este cliente?')) {
      this.clienteService.deleteCliente(id).subscribe(() => {
        this.loadClientes();
      });
    }
  }

  resetForm() {
    this.clienteModel = { id: 0, nombre: '', apellido: '', direccion: '', correo: '', passwor: '' };
    this.isEdit = false;
  }
}