import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserAccess } from './user-access';
import { shareReplay, tap } from 'rxjs/operators';
import { ReflectionUtilsService } from './reflection-utils.service';
import { ApiRoutes, ApiMeta } from './api-routes.enum';
import { Observable } from 'rxjs';

type LoginApiMeta = ApiMeta<ApiRoutes.CreateToken>;
type RegisterApiMeta = ApiMeta<ApiRoutes.Register>;

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private reflectionUtils: ReflectionUtilsService) {}

  login(username: string, password: string): Observable<UserAccess> {
    const payload: LoginApiMeta['payload'] = {
      username,
      password,
    };

    return this.http.post<LoginApiMeta['response']>(ApiRoutes.CreateToken, payload).pipe(
      tap((res) => this.setSession(res)),
      shareReplay()
    );
  }

  private setSession(user: UserAccess): void {
    localStorage.setItem(this.reflectionUtils.nameof<UserAccess>('accessToken'), user.accessToken);
    localStorage.setItem(this.reflectionUtils.nameof<UserAccess>('userId'), user.userId);
    localStorage.setItem(this.reflectionUtils.nameof<UserAccess>('username'), user.username);
  }

  register(username: string, email: string, password: string): Observable<UserAccess> {
    const payload: RegisterApiMeta['payload'] = {
      username,
      email,
      password,
    };

    return this.http.post<RegisterApiMeta['response']>(ApiRoutes.Register, payload).pipe(
      tap((res) => this.setSession(res)),
      shareReplay()
    );
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem(this.reflectionUtils.nameof<UserAccess>('accessToken'));
    return !!token;
  }

  logout(): void {
    localStorage.removeItem(this.reflectionUtils.nameof<UserAccess>('accessToken'));
    localStorage.removeItem(this.reflectionUtils.nameof<UserAccess>('userId'));
    localStorage.removeItem(this.reflectionUtils.nameof<UserAccess>('username'));
  }
}
