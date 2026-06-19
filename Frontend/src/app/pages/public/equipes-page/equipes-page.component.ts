import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EquipeService } from '../../../services/api/equipe.service';
import { Equipe } from '../../../services/api/models/equipe.model';

@Component({
  selector: 'app-equipes-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './equipes-page.component.html',
  styleUrl: './equipes-page.component.css'
})
export class EquipesPageComponent implements OnInit {
  equipes = signal<Equipe[]>([]);
  loading = signal<boolean>(true);

  constructor(private equipeService: EquipeService) {}

  ngOnInit(): void {
    this.equipeService.getAll().subscribe({
      next: (data) => {
        this.equipes.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }
}