import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../_models/user';
import { BehaviorSubject, map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'accounts/login', model).pipe(
      map((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(model: any) {
    console.log('model: ', model);
    return this.http.post<User>(this.baseUrl + 'accounts/register', model).pipe(
      map((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
        return user;
      })
    );
  }

  me() {
    const token = this.getCurrentUser()?.accessToken;
    if (!token) throw new Error('User is not authenticated');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http
      .get<any>(this.baseUrl + 'accounts/current-user-info', {
        headers,
      })
      .pipe(
        map((user) => {
          if (user) {
            this.setCurrentUser(user);
          }
          return user;
        })
      );
  }

  setCurrentUser(user: User) {
    if (user && user.accessToken) {
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUser.set(user);
    }
  }

  getCurrentUser() {
    const user = localStorage.getItem('user');
    const parsedUser: User = user ? JSON.parse(user) : null;
    return parsedUser;
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
