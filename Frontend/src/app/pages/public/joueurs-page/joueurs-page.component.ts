import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JoueurService } from '../../../services/api/joueur.service';
import { Joueur } from '../../../services/api/models/joueur.model';

@Component({
  selector: 'app-joueurs-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './joueurs-page.component.html',
  styleUrl: './joueurs-page.component.css'
})
export class JoueursPageComponent implements OnInit {
  joueurs = signal<Joueur[]>([]);
  loading = signal<boolean>(true);
  error = signal<string | null>(null);

  constructor(private joueurService: JoueurService) {}

  ngOnInit(): void {
    this.joueurService.getAll().subscribe({
      next: (data) => {
        this.joueurs.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set('Erreur lors du chargement des joueurs');
        this.loading.set(false);
      }
    });
  }
}