import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { JoueurService } from '../../../services/api/joueur.service';
import { Joueur } from '../../../services/api/models/joueur.model';

@Component({
  selector: 'app-admin-joueurs-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-joueurs-page.component.html',
  styleUrl: './admin-joueurs-page.component.css'
})
export class AdminJoueursPageComponent implements OnInit {
  joueurs = signal<Joueur[]>([]);
  loading = signal<boolean>(true);
  showForm = signal<boolean>(false);
  isEditing = signal<boolean>(false);

  formData = signal<Joueur>({
    idJoueur: 0,
    idEquipe: null,
    idPoste: null,
    nom: '',
    prenom: '',
    dateNaissance: null,
    nationalite: null,
    numeroMaillot: null
  });

  constructor(private joueurService: JoueurService) {}

  ngOnInit(): void {
    this.loadJoueurs();
  }

  loadJoueurs(): void {
    this.loading.set(true);
    this.joueurService.getAll().subscribe({
      next: (data) => {
        this.joueurs.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  openAddForm(): void {
    this.isEditing.set(false);
    this.formData.set({
      idJoueur: 0,
      idEquipe: null,
      idPoste: null,
      nom: '',
      prenom: '',
      dateNaissance: null,
      nationalite: null,
      numeroMaillot: null
    });
    this.showForm.set(true);
  }

  openEditForm(joueur: Joueur): void {
    this.isEditing.set(true);
    this.formData.set({ ...joueur });
    this.showForm.set(true);
  }

  closeForm(): void {
    this.showForm.set(false);
  }

  save(): void {
    if (this.isEditing()) {
      this.joueurService.update(this.formData().idJoueur, this.formData()).subscribe({
        next: () => {
          this.loadJoueurs();
          this.closeForm();
        }
      });
    } else {
      this.joueurService.create(this.formData()).subscribe({
        next: () => {
          this.loadJoueurs();
          this.closeForm();
        }
      });
    }
  }

  delete(id: number): void {
    if (confirm('Êtes-vous sûr de vouloir supprimer ce joueur ?')) {
      this.joueurService.delete(id).subscribe({
        next: () => this.loadJoueurs()
      });
    }
  }

  updateField(field: keyof Joueur, value: any): void {
    this.formData.update(current => ({ ...current, [field]: value }));
  }
}