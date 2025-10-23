import { Component } from '@angular/core';
import { Router, RouterOutlet,RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './core/services/auth.service';
import { AuthStateService } from './core/services/auth-state.service';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule,RouterModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isLoggedIn = true;

  constructor(private router: Router, private authService:AuthService,
    private authState: AuthStateService,
  ) {}
  ngOnInit() {
    this.authState.isLoggedIn$.subscribe(status => {
      this.isLoggedIn = status;
    });
    this.getOrCreateAnonymousId();
  }


  goToLogin() {
    this.router.navigate(['/login']);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
  toggleTheme() {
    const body = document.body;
    body.classList.toggle('light-mode');
    body.classList.toggle('dark-mode');
  }
  // utils/anonymous-id.ts
 getOrCreateAnonymousId(): string {
  let id = localStorage.getItem('anonId');
  if (!id) {
    id = crypto.randomUUID();
    localStorage.setItem('anonId', id);
  }
  return id;
}

}
