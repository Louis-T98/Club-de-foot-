import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ContratService } from '../../../services/api/contrat.service';
import { Contrat } from '../../../services/api/models/contrat.model';

@Component({
  selector: 'app-admin-contrats-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-contrats-page.component.html',
  styleUrl: './admin-contrats-page.component.css'
})
export class AdminContratsPageComponent implements OnInit {
  contrats = signal<Contrat[]>([]);
  loading = signal<boolean>(true);
  showForm = signal<boolean>(false);
  isEditing = signal<boolean>(false);

  formData = signal<Contrat>({
    idContrat: 0,
    idJoueur: 0,
    nomJoueur: null,
    dateDebut: null,
    dateFin: null,
    salaireMensuel: null
  });

  constructor(private contratService: ContratService) {}

  ngOnInit(): void {
    this.loadContrats();
  }

  loadContrats(): void {
    this.loading.set(true);
    this.contratService.getAll().subscribe({
      next: (data) => {
        this.contrats.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  openAddForm(): void {
    this.isEditing.set(false);
    this.formData.set({
      idContrat: 0,
      idJoueur: 0,
      nomJoueur: null,
      dateDebut: null,
      dateFin: null,
      salaireMensuel: null
    });
    this.showForm.set(true);
  }

  openEditForm(contrat: Contrat): void {
    this.isEditing.set(true);
    this.formData.set({ ...contrat });
    this.showForm.set(true);
  }

  closeForm(): void {
    this.showForm.set(false);
  }

  save(): void {
    if (this.isEditing()) {
      this.contratService.update(this.formData().idContrat, this.formData()).subscribe({
        next: () => {
          this.loadContrats();
          this.closeForm();
        }
      });
    } else {
      this.contratService.create(this.formData()).subscribe({
        next: () => {
          this.loadContrats();
          this.closeForm();
        }
      });
    }
  }

  delete(id: number): void {
    if (confirm('Êtes-vous sûr de vouloir supprimer ce contrat ?')) {
      this.contratService.delete(id).subscribe({
        next: () => this.loadContrats()
      });
    }
  }

  updateField(field: keyof Contrat, value: any): void {
    this.formData.update(current => ({ ...current, [field]: value }));
  }
}