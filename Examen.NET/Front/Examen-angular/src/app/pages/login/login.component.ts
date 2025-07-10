import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClienteService } from '../../services/cliente.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import('@angular/common').then(m => m.CommonModule),
import('@angular/forms').then(m => m.ReactiveFormsModule)
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule,CommonModule,ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: true
})
export class LoginComponent {
  loginForm: FormGroup;
  isRegister = false;

  constructor(
    private fb: FormBuilder,
    private clienteService: ClienteService,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      correo: ['', [Validators.required, Validators.email]],
      passwor: ['', Validators.required],
      nombre: [''],
      apellido: [''],
      direccion: ['']
    });
  }

  toggleRegister() {
    this.isRegister = !this.isRegister;
    this.loginForm.reset();
  }

  onSubmit() {
    if (this.isRegister) {
      if (
        !this.loginForm.get('nombre')?.value ||
        !this.loginForm.get('apellido')?.value ||
        !this.loginForm.get('direccion')?.value
      ) {
        alert('Por favor completa todos los campos de registro');
        return;
      }

      const nuevoCliente = {
        nombre: this.loginForm.get('nombre')?.value,
        apellido: this.loginForm.get('apellido')?.value,
        direccion: this.loginForm.get('direccion')?.value,
        correo: this.loginForm.get('correo')?.value,
        passwor: this.loginForm.get('passwor')?.value
      };

      this.clienteService.createCliente(nuevoCliente).subscribe({
        next: () => {
          alert('Cliente registrado correctamente.');
          this.isRegister = false;
          this.loginForm.reset();
        },
        error: err => {
          console.error('Error en registro:', err);
          alert('Error al registrar cliente: ' + (err.error?.message || err.message));
        }
      });
    } else {
      const { correo, passwor } = this.loginForm.value;

      this.clienteService.login(correo, passwor).subscribe({
        next: cliente => {
          if (cliente) {
            this.authService.login(cliente);

            alert('Bienvenido!');
            if (this.authService.isAdmin()) {
              this.router.navigate(['/clientes']);
            } else {
              this.router.navigate(['/articulos']);
            }
          } else {
            alert('Credenciales incorrectas');
          }
        },
        error: () => alert('Error al iniciar sesión.')
      });
    }
  }
}
