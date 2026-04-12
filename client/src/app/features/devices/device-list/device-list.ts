import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../core/services/user.service';
import { DeviceService } from '../../../core/services/device.service';
import { Device } from '../../../core/models/device.model';
import { User } from '../../../core/models/user.model';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-device-list',
  imports: [CommonModule, RouterModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './device-list.html',
  styleUrl: './device-list.scss',
})
export class DeviceList {
  devices$: Observable<Device[]>;
  users: User[] = [];
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
    private deviceService: DeviceService,
    private userService: UserService,
  ) {
    this.devices$ = this.deviceService.getAll();
    this.userService.getAll().subscribe((users) => (this.users = users));
  }
  getUserName(userId: number | null): string {
    if (!userId) return 'Unassigned';
    return this.users.find((u) => u.id === userId)?.name ?? 'Unassigned';
  }

  deleteDevice(id: number): void {
    this.deviceService.delete(id).subscribe(() => {
      this.devices$ = this.deviceService.getAll();
    });
  }
}
