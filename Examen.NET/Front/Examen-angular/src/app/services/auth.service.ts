import { Injectable } from '@angular/core';
import { Cliente } from './cliente.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUser: Cliente | null = null;
  private _tokenKey = 'token'; 

  login(user: Cliente) {
    this.currentUser = user;
    localStorage.setItem('user', JSON.stringify(user));
  }

  logout() {
    this.currentUser = null;
    localStorage.removeItem('user');
    sessionStorage.clear(); 
  }

  getUser(): Cliente | null {
    if (!this.currentUser) {
      const userString = localStorage.getItem('user');
      if (userString) {
        this.currentUser = JSON.parse(userString);
      }
    }
    return this.currentUser;
  }

  getToken(): string | null {
    return sessionStorage.getItem(this._tokenKey);
  }

  isAdmin(): boolean {
     const user = this.getUser();
    return user?.correo?.toLowerCase() === 'admin@admin.com';
  }

  isLoggedIn(): boolean {
    return this.getUser() != null;
  }
}