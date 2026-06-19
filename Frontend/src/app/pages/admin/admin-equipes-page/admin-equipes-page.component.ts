import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EquipeService } from '../../../services/api/equipe.service';
import { Equipe } from '../../../services/api/models/equipe.model';

@Component({
  selector: 'app-admin-equipes-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-equipes-page.component.html',
  styleUrl: './admin-equipes-page.component.css'
})
export class AdminEquipesPageComponent implements OnInit {
  equipes = signal<Equipe[]>([]);
  loading = signal<boolean>(true);
  showForm = signal<boolean>(false);
  isEditing = signal<boolean>(false);

  formData = signal<Equipe>({
    idEquipe: 0,
    idClub: 0,
    nom: '',
    categorie: null
  });

  constructor(private equipeService: EquipeService) {}

  ngOnInit(): void {
    this.loadEquipes();
  }

  loadEquipes(): void {
    this.loading.set(true);
    this.equipeService.getAll().subscribe({
      next: (data) => {
        this.equipes.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  openAddForm(): void {
    this.isEditing.set(false);
    this.formData.set({ idEquipe: 0, idClub: 0, nom: '', categorie: null });
    this.showForm.set(true);
  }

  openEditForm(equipe: Equipe): void {
    this.isEditing.set(true);
    this.formData.set({ ...equipe });
    this.showForm.set(true);
  }

  closeForm(): void {
    this.showForm.set(false);
  }

  save(): void {
    if (this.isEditing()) {
      this.equipeService.update(this.formData().idEquipe, this.formData()).subscribe({
        next: () => {
          this.loadEquipes();
          this.closeForm();
        }
      });
    } else {
      this.equipeService.create(this.formData()).subscribe({
        next: () => {
          this.loadEquipes();
          this.closeForm();
        }
      });
    }
  }

  delete(id: number): void {
    if (confirm('Êtes-vous sûr de vouloir supprimer cette équipe ?')) {
      this.equipeService.delete(id).subscribe({
        next: () => this.loadEquipes()
      });
    }
  }

  updateField(field: keyof Equipe, value: any): void {
    this.formData.update(current => ({ ...current, [field]: value }));
  }
}