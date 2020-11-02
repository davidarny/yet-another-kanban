import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserAccess } from '../models/user-access';
import { shareReplay, tap } from 'rxjs/operators';
import { ReflectionUtilsService } from '../utils/reflection/reflection-utils.service';
import { ApiRoutes, ApiMeta } from '../router/api-routes.enum';
import { BehaviorSubject, Observable } from 'rxjs';

type LoginApiMeta = ApiMeta<ApiRoutes.CreateToken>;
type RegisterApiMeta = ApiMeta<ApiRoutes.Register>;

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private logged$ = new BehaviorSubject<boolean>(false);

  get loggedIn$(): Observable<boolean> {
    return this.logged$.asObservable();
  }

  constructor(private http: HttpClient, private reflectionUtils: ReflectionUtilsService) {
    if (this.isLoggedIn()) {
      this.logged$.next(true);
    }
  }

  login(username: string, password: string): Observable<UserAccess> {
    const payload: LoginApiMeta['payload'] = {
      username,
      password,
    };

    return this.http.post<LoginApiMeta['response']>(ApiRoutes.CreateToken, payload).pipe(
      tap((res) => this.setSession(res)),
      tap(() => this.logged$.next(true)),
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

  isLoggedIn(): boolean {
    return !!(
      localStorage.getItem(this.reflectionUtils.nameof<UserAccess>('accessToken')) &&
      localStorage.getItem(this.reflectionUtils.nameof<UserAccess>('userId')) &&
      localStorage.getItem(this.reflectionUtils.nameof<UserAccess>('username'))
    );
  }

  logout(): void {
    localStorage.removeItem(this.reflectionUtils.nameof<UserAccess>('accessToken'));
    localStorage.removeItem(this.reflectionUtils.nameof<UserAccess>('userId'));
    localStorage.removeItem(this.reflectionUtils.nameof<UserAccess>('username'));

    this.logged$.next(false);
  }
}
