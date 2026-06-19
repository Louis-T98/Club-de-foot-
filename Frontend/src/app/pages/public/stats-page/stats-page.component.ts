import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatchService } from '../../../services/api/match.service';
import { StatistiqueService } from '../../../services/api/statistique.service';
import { Match } from '../../../services/api/models/match.model';
import { Statistique } from '../../../services/api/models/statistique.model';

@Component({
  selector: 'app-stats-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stats-page.component.html',
  styleUrl: './stats-page.component.css'
})
export class StatsPageComponent implements OnInit {
  matchs = signal<Match[]>([]);
  statistiques = signal<Statistique[]>([]);
  matchSelectionne = signal<Match | null>(null);
  loading = signal<boolean>(true);

  constructor(
    private matchService: MatchService,
    private statistiqueService: StatistiqueService
  ) {}

  ngOnInit(): void {
    this.matchService.getAll().subscribe({
      next: (data) => {
        this.matchs.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  selectMatch(match: Match): void {
    this.matchSelectionne.set(match);
    this.statistiqueService.getByMatch(match.idMatch).subscribe({
      next: (data) => this.statistiques.set(data)
    });
  }
}