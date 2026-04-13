import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { AuthService } from '../../../core/services/auth.service';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MatIcon,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  loginData = { email: '', password: '' };
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  login(): void {
    this.authService.login(this.loginData).subscribe({
      next: () => this.router.navigate(['/devices']),
      error: (err) => {
  if (err.status === 400) {
    if (typeof err.error === 'string') {
      this.errorMessage = err.error; 
    } else {
      this.errorMessage = 'Please fill in all required fields.';
    }
  } else {
    this.errorMessage = 'Something went wrong. Please try again.';
  }
   this.cdr.detectChanges();
}
    });
  }
}
