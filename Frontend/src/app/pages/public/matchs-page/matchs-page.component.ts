import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatchService } from '../../../services/api/match.service';
import { Match } from '../../../services/api/models/match.model';

@Component({
  selector: 'app-matchs-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './matchs-page.component.html',
  styleUrl: './matchs-page.component.css'
})
export class MatchsPageComponent implements OnInit {
  matchs = signal<Match[]>([]);
  loading = signal<boolean>(true);

  constructor(private matchService: MatchService) {}

  ngOnInit(): void {
    this.matchService.getAll().subscribe({
      next: (data) => {
        this.matchs.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  getResultat(match: Match): string {
    if (match.scorePour === null || match.scoreContre === null) return 'nul';
    if (match.scorePour > match.scoreContre) return 'victoire';
    if (match.scorePour < match.scoreContre) return 'defaite';
    return 'nul';
  }
}