import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Blessure } from './models/blessure.model';

@Injectable({
  providedIn: 'root'
})
export class BlessureService {
  private apiUrl = 'http://localhost:5207/api/blessures';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Blessure[]> {
    return this.http.get<Blessure[]>(this.apiUrl);
  }

  getByJoueur(idJoueur: number): Observable<Blessure[]> {
    return this.http.get<Blessure[]>(`${this.apiUrl}/joueur/${idJoueur}`);
  }

  create(blessure: Blessure): Observable<Blessure> {
    return this.http.post<Blessure>(this.apiUrl, blessure);
  }

  update(id: number, blessure: Blessure): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, blessure);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}