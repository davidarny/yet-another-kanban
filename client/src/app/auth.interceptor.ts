import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReflectionUtilsService } from './reflection-utils.service';
import { UserAccess } from './user-access';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private reflectionUtils: ReflectionUtilsService) {}

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem(this.reflectionUtils.nameof<UserAccess>('accessToken'));

    if (token) {
      const cloned = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + token),
      });

      return next.handle(cloned);
    } else {
      return next.handle(req);
    }
  }
}
