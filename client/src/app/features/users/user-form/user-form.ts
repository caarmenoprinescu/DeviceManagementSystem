import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../core/services/user.service';
import { UserRequest } from '../../../core/models/user.model';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './user-form.html',
  styleUrl: './user-form.scss',
})
export class UserForm {
  user: UserRequest = {
    name: '',
    role: '',
    location: '',
  };

  id: number = 0;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      this.id = +idParam;

      this.userService.getById(this.id).subscribe({
        next: (user) => {
          this.user.name = user.name;
          this.user.role = user.role;
          this.user.location = user.location;
        },
        error: () => {
          alert('Error loading user');
          this.router.navigate(['/users']);
        },
      });
    } else {
      this.router.navigate(['/users']);
    }
  }

  onSubmit(): void {
    if (!this.user.name || !this.user.role || !this.user.location) {
      alert('Please fill all required fields');
      return;
    }
    this.update();
  }

  update(): void {
    this.userService.update(this.user, this.id).subscribe({
      next: () => {
        alert('User updated successfully!');
        this.router.navigate(['/users']);
      },
      error: (err) => {
        console.error(err);
        alert('Error updating user');
      },
    });
  }
}