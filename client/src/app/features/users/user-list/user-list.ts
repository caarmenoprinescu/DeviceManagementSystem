import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../core/services/user.service';
import { User } from '../../../core/models/user.model';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-user-list',
  imports: [CommonModule, RouterModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './user-list.html',
  styleUrl: './user-list.scss',
})
export class UserList {
  users$: Observable<User[]>;
  users: User[] = [];
  displayedColumns: string[] = [];

ngOnInit() {
  const id = Number(localStorage.getItem('userId'));

  this.userService.getById(id).subscribe(user => {
    this.isAdmin = user?.role?.toLowerCase() === 'admin';

    this.displayedColumns = ['name', 'role', 'location'];

    if (this.isAdmin) {
      this.displayedColumns.push('actions');
    }
  });
}
  constructor(private userService: UserService) {
    this.users$ = this.userService.getAll();
    this.userService.getAll().subscribe((users) => (this.users = users));
  }
  isAdmin: boolean = false;


  getUserName(userId: number | null): string {
    if (!userId) return 'Unassigned';
    return this.users.find((u) => u.id === userId)?.name ?? 'Unassigned';
  }

  deleteUser(id: number): void {
    this.userService.delete(id).subscribe(() => {
      this.users$ = this.userService.getAll();
    });
  }

  
  }
