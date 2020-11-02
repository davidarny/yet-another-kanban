import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ReflectionUtilsService {
  constructor() {}

  nameof<T>(name: keyof T): keyof T {
    return name;
  }
}
