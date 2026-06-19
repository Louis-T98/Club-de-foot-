import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BlessureService } from '../../../services/api/blessure.service';
import { Blessure } from '../../../services/api/models/blessure.model';

@Component({
  selector: 'app-admin-blessures-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-blessures-page.component.html',
  styleUrl: './admin-blessures-page.component.css'
})
export class AdminBlessuresPageComponent implements OnInit {
  blessures = signal<Blessure[]>([]);
  loading = signal<boolean>(true);
  showForm = signal<boolean>(false);
  isEditing = signal<boolean>(false);

  formData = signal<Blessure>({
    idBlessure: 0,
    idJoueur: 0,
    nomJoueur: null,
    typeBlessure: null,
    dateDebut: null,
    dateFin: null
  });

  constructor(private blessureService: BlessureService) {}

  ngOnInit(): void {
    this.loadBlessures();
  }

  loadBlessures(): void {
    this.loading.set(true);
    this.blessureService.getAll().subscribe({
      next: (data) => {
        this.blessures.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  openAddForm(): void {
    this.isEditing.set(false);
    this.formData.set({
      idBlessure: 0,
      idJoueur: 0,
      nomJoueur: null,
      typeBlessure: null,
      dateDebut: null,
      dateFin: null
    });
    this.showForm.set(true);
  }

  openEditForm(blessure: Blessure): void {
    this.isEditing.set(true);
    this.formData.set({ ...blessure });
    this.showForm.set(true);
  }

  closeForm(): void {
    this.showForm.set(false);
  }

  save(): void {
    if (this.isEditing()) {
      this.blessureService.update(this.formData().idBlessure, this.formData()).subscribe({
        next: () => {
          this.loadBlessures();
          this.closeForm();
        }
      });
    } else {
      this.blessureService.create(this.formData()).subscribe({
        next: () => {
          this.loadBlessures();
          this.closeForm();
        }
      });
    }
  }

  delete(id: number): void {
    if (confirm('Êtes-vous sûr de vouloir supprimer cette blessure ?')) {
      this.blessureService.delete(id).subscribe({
        next: () => this.loadBlessures()
      });
    }
  }

  updateField(field: keyof Blessure, value: any): void {
    this.formData.update(current => ({ ...current, [field]: value }));
  }
}