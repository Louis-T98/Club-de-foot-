import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Joueur } from './models/joueur.model';

@Injectable({
  providedIn: 'root'
})
export class JoueurService {
  private apiUrl = 'http://localhost:5207/api/joueurs';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Joueur[]> {
    return this.http.get<Joueur[]>(this.apiUrl);
  }

  getById(id: number): Observable<Joueur> {
    return this.http.get<Joueur>(`${this.apiUrl}/${id}`);
  }

  create(joueur: Joueur): Observable<Joueur> {
    return this.http.post<Joueur>(this.apiUrl, joueur);
  }

  update(id: number, joueur: Joueur): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, joueur);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}