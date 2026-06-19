import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/api/auth.service';
import { AuthStateService } from '../../../services/auth-state.service';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginPageComponent {
  username = signal<string>('');
  password = signal<string>('');
  error = signal<string | null>(null);

  constructor(
    private authService: AuthService,
    private authState: AuthStateService,
    private router: Router
  ) {}

  login(): void {
    this.authService.login({
      username: this.username(),
      password: this.password()
    }).subscribe({
      next: (response) => {
        this.authState.setToken(response.token);
        this.router.navigate(['/admin']);
      },
      error: () => {
        this.error.set('Identifiants incorrects');
      }
    });
  }
}