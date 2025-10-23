import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule,RouterModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  form = {
    username: '',
    email: '',
    password: '',
    confirmPassword: ''
  };  
  error=""; 
  
  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    if (!this.form.username || !this.form.email || !this.form.password) {
      this.error = 'All fields are required';
      return;
    }
    else if (this.form.confirmPassword!=this.form.password){
      this.error = 'Password doenot match';
      return;
    }

    this.authService.register({ username: this.form.username, email: this.form.email, password: this.form.confirmPassword })
      .subscribe({
        
            next: (res) => {
      console.log(res.message); // should print "Registration successful"
      this.router.navigate(['/login']);
    },
    error: err => {
      console.error(err);
      this.error = err.error?.message || 'Registration failed';
    }
  });

  }
}
