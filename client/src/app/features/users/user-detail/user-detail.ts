import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../core/services/user.service';
import { User } from '../../../core/models/user.model';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatDivider } from '@angular/material/divider';

@Component({
  selector: 'app-user-detail',
  imports: [CommonModule, RouterModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './user-detail.html',
  styleUrl: './user-detail.scss',
})
export class UserDetail {
  user$: Observable<User>;

  displayedColumns = [
    'name',
    'manufacturer',
    'type',
    'operatingSystem',
    'osVersion',
    'processor',
    'ram',
    'description',
    'assignedUser',
    'actions',
  ];

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.user$ = this.userService.getById(id);
  }

}
