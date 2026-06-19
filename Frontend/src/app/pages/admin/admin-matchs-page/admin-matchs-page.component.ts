import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatchService } from '../../../services/api/match.service';
import { Match } from '../../../services/api/models/match.model';

@Component({
  selector: 'app-admin-matchs-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-matchs-page.component.html',
  styleUrl: './admin-matchs-page.component.css'
})
export class AdminMatchsPageComponent implements OnInit {
  matchs = signal<Match[]>([]);
  loading = signal<boolean>(true);
  showForm = signal<boolean>(false);
  isEditing = signal<boolean>(false);

  formData = signal<Match>({
    idMatch: 0,
    idEquipe: null,
    adversaire: null,
    dateMatch: null,
    scorePour: null,
    scoreContre: null
  });

  constructor(private matchService: MatchService) {}

  ngOnInit(): void {
    this.loadMatchs();
  }

  loadMatchs(): void {
    this.loading.set(true);
    this.matchService.getAll().subscribe({
      next: (data) => {
        this.matchs.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  openAddForm(): void {
    this.isEditing.set(false);
    this.formData.set({
      idMatch: 0,
      idEquipe: null,
      adversaire: null,
      dateMatch: null,
      scorePour: null,
      scoreContre: null
    });
    this.showForm.set(true);
  }

  openEditForm(match: Match): void {
    this.isEditing.set(true);
    this.formData.set({ ...match });
    this.showForm.set(true);
  }

  closeForm(): void {
    this.showForm.set(false);
  }

  save(): void {
    if (this.isEditing()) {
      this.matchService.update(this.formData().idMatch, this.formData()).subscribe({
        next: () => {
          this.loadMatchs();
          this.closeForm();
        }
      });
    } else {
      this.matchService.create(this.formData()).subscribe({
        next: () => {
          this.loadMatchs();
          this.closeForm();
        }
      });
    }
  }

  delete(id: number): void {
    if (confirm('Êtes-vous sûr de vouloir supprimer ce match ?')) {
      this.matchService.delete(id).subscribe({
        next: () => this.loadMatchs()
      });
    }
  }

  updateField(field: keyof Match, value: any): void {
    this.formData.update(current => ({ ...current, [field]: value }));
  }
}