import {  Injectable, Inject, Optional, InjectionToken  } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Employee } from './employee';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
      private http: HttpClient;
      private baseUrl: string;
      protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;
  
      constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
          this.http = http;
          this.baseUrl = baseUrl ?? "";
      }
  
  // private apiURL = "https://localhost:5001/api";


  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  // constructor(private httpClient: HttpClient) { }

  getAll(pageNumber: number = 1, pageSize: number = 20): Observable<any> {
    const url = `${this.baseUrl}/api/Employee?PageNumber=${pageNumber}&PageSize=${pageSize}`;

    return this.http.get(url, this.httpOptions)
      .pipe(
        catchError(this.errorHandler)
      );
  }


  create(employee: Employee): Observable<any> {

    return this.http.post(this.baseUrl + '/api/Employee/', JSON.stringify(employee), this.httpOptions)

      .pipe(
        catchError(this.errorHandler)
      )
  }


  find(id: number): Observable<any> {

    return this.http.get(this.baseUrl + '/api/Employee/' + id)

      .pipe(
        catchError(this.errorHandler)
      )
  }


  update(id: number, employee: Employee): Observable<any> {

    return this.http.put(this.baseUrl + '/api/Employee/' + id, JSON.stringify(employee), this.httpOptions)

      .pipe(
        catchError(this.errorHandler)
      )
  }


  delete(id: number) {
    return this.http.delete(this.baseUrl + '/api/Employee/' + id, this.httpOptions)

      .pipe(
        catchError(this.errorHandler)
      )
  }


  errorHandler(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }
}
