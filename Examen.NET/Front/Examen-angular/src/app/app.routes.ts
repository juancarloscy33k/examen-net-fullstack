import { Routes } from '@angular/router';
import { ClientesComponent } from './pages/clientes/clientes.component';
import { LoginComponent } from './pages/login/login.component';
import { TiendasComponent } from './pages/tiendas/tiendas.component';
import { ArticulosComponent } from './pages/articulos/articulos.component';
import { CarritoComponent } from './pages/carrito/carrito.component';
import { AdminGuard } from './services/admin.guard';
import { CompraComponent } from './pages/compra/compra.component';

export const routes: Routes = [
{ path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'clientes', component: ClientesComponent, canActivate: [AdminGuard] },
  { path: 'tiendas', component: TiendasComponent, canActivate: [AdminGuard] },
  { path: 'articulos', component: ArticulosComponent },
  { path: 'carrito', component: CarritoComponent },
  { path: 'compras', component: CompraComponent },
];
