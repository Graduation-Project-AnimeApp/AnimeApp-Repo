import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { AuthResponse } from "../model/user";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  http = inject(HttpClient);
  baseUrl = "https://localhost:7059/api";

  login(userData: object): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.baseUrl}/Auth/login`, userData)
      .pipe(
        tap((res) => {
          localStorage.setItem("userId", res.user.id.toString());
          localStorage.setItem("username", res.user.username.toString());
        })
      );
  }

  signup(userData: object): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.baseUrl}/Auth/register`, userData)
      .pipe(
        tap((res) => {
          localStorage.setItem("userId", res.user.id.toString());
          localStorage.setItem("username", res.user.username.toString());
        })
      );
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem("userId");
  }
  getUserId(): string | null {
    return localStorage.getItem("userId");
  }
}
