import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalDataService {

  constructor() { }

  get<T>(key: string): T {
    return JSON.parse(localStorage.getItem(key)) as T;
  }

  set(key: string, data: any): void {
    localStorage.setItem(key, JSON.stringify(data));
  }

  remove(key: string) {
    localStorage.removeItem(key);
  }
}
