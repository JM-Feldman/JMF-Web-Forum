import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../Services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerData = {
    username: '',
    password: '',
    confirmPassword: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  register() {
    if (this.registerData.password !== this.registerData.confirmPassword) {
      alert('Passwords do not match.');
      return;
    }

    this.authService.register(this.registerData).subscribe(
      response => {
        localStorage.setItem('token', response.token);
        this.router.navigate(['/home']); 
      },
      error => {
        console.error(error);
        alert('Registration failed. Please try again.');
      }
    );
  }
}



