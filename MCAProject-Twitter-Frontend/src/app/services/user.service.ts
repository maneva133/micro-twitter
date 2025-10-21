import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap } from 'rxjs';
import { environment } from '../../enviorments/enviorment';
import { RegisterDto, LoginDto } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private base = `${environment.apiUrl}/Users`;

  private currentUserSubject = new BehaviorSubject<string | null>(localStorage.getItem('currentUser'));
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {}

  setCurrentUser(username: string) {
    localStorage.setItem('currentUser', username);
    this.currentUserSubject.next(username);
  }

  getCurrentUser(): string | null {
    return this.currentUserSubject.value;
  }

  isLoggedIn(): boolean {
    return !!this.currentUserSubject.value;
  }

  logout(): void {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  register(dto: RegisterDto): Observable<any> {
    return this.http.post(`${this.base}/register`, dto).pipe(
      tap(() => this.setCurrentUser(dto.username))
    );
  }

  login(dto: LoginDto): Observable<any> {
    return this.http.post<any>(`${this.base}/login`, dto).pipe(
      tap((res) => this.setCurrentUser(res.user))
    );
  }
}
