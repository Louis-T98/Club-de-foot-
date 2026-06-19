import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Statistique } from './models/statistique.model';

@Injectable({
  providedIn: 'root'
})
export class StatistiqueService {
  private apiUrl = 'http://localhost:5207/api/statistiques';

  constructor(private http: HttpClient) {}

  getByMatch(idMatch: number): Observable<Statistique[]> {
    return this.http.get<Statistique[]>(`${this.apiUrl}/match/${idMatch}`);
  }

  getByJoueur(idJoueur: number): Observable<Statistique[]> {
    return this.http.get<Statistique[]>(`${this.apiUrl}/joueur/${idJoueur}`);
  }

  create(statistique: Statistique): Observable<Statistique> {
    return this.http.post<Statistique>(this.apiUrl, statistique);
  }

  update(statistique: Statistique): Observable<void> {
    return this.http.put<void>(this.apiUrl, statistique);
  }

  delete(idMatch: number, idJoueur: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${idMatch}/${idJoueur}`);
  }
}