import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './user';
import { shareReplay, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  login(username: string, password: string) {
    return this.http
      .post<User>('/api/users/token', { username, password })
      .pipe(
        tap((res) => this.setSession(res)),
        shareReplay()
      );
  }

  private setSession(user: User) {
    localStorage.setItem('access_token', user.access_token);
    localStorage.setItem('id_user', user.id_user);
    localStorage.setItem('username', user.username);
  }

  isAuthenticated() {
    const token = localStorage.getItem('access_token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('id_user');
    localStorage.removeItem('username');
  }
}
