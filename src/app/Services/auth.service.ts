import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7100/api/auth';

  constructor(private http: HttpClient) {}

  login(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, data);
  }

  register(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, data);
  }
}

