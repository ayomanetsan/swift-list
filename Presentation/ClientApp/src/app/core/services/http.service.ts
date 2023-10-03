import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private baseUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  get<T>(url: string): Observable<T> {
    return this.httpClient.get<T>(this.buildUrl(url), this.getHeaders()).pipe(catchError(this.handleError));
  }

  post<T>(url: string, resource: unknown) {
    return this.httpClient.post<T>(this.buildUrl(url), resource, this.getHeaders()).pipe(catchError(this.handleError));
  }

  delete(url: string, id: string | number) {
    return this.httpClient.delete(`${this.buildUrl(url)}/${id}`, this.getHeaders()).pipe(catchError(this.handleError));
  }

  put<T>(url: string, resource: T) {
    return this.httpClient.put<T>(this.buildUrl(url), resource, this.getHeaders()).pipe(catchError(this.handleError));
  }

  private handleError(err: HttpErrorResponse) {
    return throwError(() => err);
  }

  private buildUrl(url: string): string {
    return this.baseUrl + url;
  }

  private getHeaders(): { headers?: HttpHeaders } {
    const token = localStorage.getItem('token');
    if (token) {
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });
      return { headers };
    }
    return {};
  }  
}
