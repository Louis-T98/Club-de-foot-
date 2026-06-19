import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StaffService } from '../../../services/api/staff.service';
import { Staff } from '../../../services/api/models/staff.model';

@Component({
  selector: 'app-admin-staff-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-staff-page.component.html',
  styleUrl: './admin-staff-page.component.css'
})
export class AdminStaffPageComponent implements OnInit {
  staffList = signal<Staff[]>([]);
  loading = signal<boolean>(true);
  showForm = signal<boolean>(false);
  isEditing = signal<boolean>(false);

  formData = signal<Staff>({
    idStaff: 0,
    idEquipe: null,
    nom: '',
    prenom: '',
    role: null
  });

  constructor(private staffService: StaffService) {}

  ngOnInit(): void {
    this.loadStaff();
  }

  loadStaff(): void {
    this.loading.set(true);
    this.staffService.getAll().subscribe({
      next: (data) => {
        this.staffList.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  openAddForm(): void {
    this.isEditing.set(false);
    this.formData.set({
      idStaff: 0,
      idEquipe: null,
      nom: '',
      prenom: '',
      role: null
    });
    this.showForm.set(true);
  }

  openEditForm(staff: Staff): void {
    this.isEditing.set(true);
    this.formData.set({ ...staff });
    this.showForm.set(true);
  }

  closeForm(): void {
    this.showForm.set(false);
  }

  save(): void {
    if (this.isEditing()) {
      this.staffService.update(this.formData().idStaff, this.formData()).subscribe({
        next: () => {
          this.loadStaff();
          this.closeForm();
        }
      });
    } else {
      this.staffService.create(this.formData()).subscribe({
        next: () => {
          this.loadStaff();
          this.closeForm();
        }
      });
    }
  }

  delete(id: number): void {
    if (confirm('Êtes-vous sûr de vouloir supprimer ce membre du staff ?')) {
      this.staffService.delete(id).subscribe({
        next: () => this.loadStaff()
      });
    }
  }

  updateField(field: keyof Staff, value: any): void {
    this.formData.update(current => ({ ...current, [field]: value }));
  }
}