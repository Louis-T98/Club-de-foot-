import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthStateService } from '../../../services/auth-state.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-page',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './dashboard-page.component.html',
  styleUrl: './dashboard-page.component.css'
})
export class DashboardPageComponent {
  constructor(
    private authState: AuthStateService,
    private router: Router
  ) {}

  logout(): void {
    this.authState.logout();
    this.router.navigate(['/login']);
  }
}