import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Entrainement } from './models/entrainement.model';

@Injectable({
  providedIn: 'root'
})
export class EntrainementService {
  private apiUrl = 'http://localhost:5207/api/entrainements';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Entrainement[]> {
    return this.http.get<Entrainement[]>(this.apiUrl);
  }

  getById(id: number): Observable<Entrainement> {
    return this.http.get<Entrainement>(`${this.apiUrl}/${id}`);
  }

  create(entrainement: Entrainement): Observable<Entrainement> {
    return this.http.post<Entrainement>(this.apiUrl, entrainement);
  }

  update(id: number, entrainement: Entrainement): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, entrainement);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}