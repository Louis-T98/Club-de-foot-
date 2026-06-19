import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EntrainementService } from '../../../services/api/entrainement.service';
import { Entrainement } from '../../../services/api/models/entrainement.model';

@Component({
  selector: 'app-admin-entrainements-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-entrainements-page.component.html',
  styleUrl: './admin-entrainements-page.component.css'
})
export class AdminEntrainementsPageComponent implements OnInit {
  entrainements = signal<Entrainement[]>([]);
  loading = signal<boolean>(true);
  showForm = signal<boolean>(false);
  isEditing = signal<boolean>(false);

  formData = signal<Entrainement>({
    idEntrainement: 0,
    idEquipe: null,
    nomEquipe: null,
    dateEntrainement: null,
    dureeMinutes: null,
    theme: null
  });

  constructor(private entrainementService: EntrainementService) {}

  ngOnInit(): void {
    this.loadEntrainements();
  }

  loadEntrainements(): void {
    this.loading.set(true);
    this.entrainementService.getAll().subscribe({
      next: (data) => {
        this.entrainements.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  openAddForm(): void {
    this.isEditing.set(false);
    this.formData.set({
      idEntrainement: 0,
      idEquipe: null,
      nomEquipe: null,
      dateEntrainement: null,
      dureeMinutes: null,
      theme: null
    });
    this.showForm.set(true);
  }

  openEditForm(entrainement: Entrainement): void {
    this.isEditing.set(true);
    this.formData.set({ ...entrainement });
    this.showForm.set(true);
  }

  closeForm(): void {
    this.showForm.set(false);
  }

  save(): void {
    if (this.isEditing()) {
      this.entrainementService.update(this.formData().idEntrainement, this.formData()).subscribe({
        next: () => {
          this.loadEntrainements();
          this.closeForm();
        }
      });
    } else {
      this.entrainementService.create(this.formData()).subscribe({
        next: () => {
          this.loadEntrainements();
          this.closeForm();
        }
      });
    }
  }

  delete(id: number): void {
    if (confirm('Êtes-vous sûr de vouloir supprimer cet entraînement ?')) {
      this.entrainementService.delete(id).subscribe({
        next: () => this.loadEntrainements()
      });
    }
  }

  updateField(field: keyof Entrainement, value: any): void {
    this.formData.update(current => ({ ...current, [field]: value }));
  }
}