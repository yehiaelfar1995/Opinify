import { Component, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule,RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  encapsulation: ViewEncapsulation.None // allow global dark mode styles
})
export class LoginComponent {
  username = '';
  password = '';
  error = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    if (!this.username || !this.password) {
      this.error = 'Username and password are required';
      return;
    }

    this.authService.login({ username: this.username, password: this.password })
      .subscribe({
        next: (res) => {
          localStorage.setItem('authToken', res.token);
          this.router.navigate(['/']);
        },
        error: (err) => {
          if (err.error && err.error.message) {
            this.error = err.error.message;
          } else if (typeof err.error === 'string') {
            this.error = err.error;
          } else {
            this.error = 'Login failed. Please try again.';
          }
        }
      });
  }

  goHome() {
    this.router.navigate(['/']);
  }
}
