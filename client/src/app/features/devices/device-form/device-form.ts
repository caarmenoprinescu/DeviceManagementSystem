import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeviceService } from '../../../core/services/device.service';
import { DeviceRequest } from '../../../core/models/device.model';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { User } from '../../../core/models/user.model';
import { UserService } from '../../../core/services/user.service';

@Component({
  selector: 'app-device-form',
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
  templateUrl: './device-form.html',
  styleUrl: './device-form.scss',
})
export class DeviceForm {
  device: DeviceRequest = {
    name: '',
    manufacturer: '',
    type: 0,
    operatingSystem: '',
    osVersion: '',
    processor: '',
    ram: 0,
    description: '',
    userId: null,
  };

  id: number = 0;
  isEditMode: boolean = false;
  users: User[] = [];
  constructor(
    private deviceService: DeviceService,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.userService.getAll().subscribe((users) => (this.users = users));
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      this.id = +idParam;
      this.isEditMode = true;

      this.deviceService.getById(this.id).subscribe({
        next: (device) => {
          this.device.name = device.name;
          this.device.manufacturer = device.manufacturer;
          this.device.type = device.type;
          this.device.operatingSystem = device.operatingSystem;
          this.device.osVersion = device.osVersion;
          this.device.processor = device.processor;
          this.device.ram = device.ram;
          this.device.description = device.description;
          this.device.userId = device.userId;
        },
        error: () => {
          alert('Error loading device');
        },
      });
    }
  }

  onSubmit(): void {
    if (!this.device.name || !this.device.manufacturer) {
      alert('Please fill all required fields');
      return;
    }

    if (this.isEditMode) {
      this.update();
    } else {
      this.create();
    }
  }

  create(): void {
  this.deviceService.create(this.device).subscribe({
    next: () => {
      alert("Device created successfully!");
      this.router.navigate(['/devices']);
    },
    error: (err) => {
      alert(err.error); 
    }
  });
}
  update(): void {
    this.deviceService.update(this.id, this.device).subscribe({
      next: () => {
        alert('Device updated successfully!');
        this.router.navigate(['/devices']);
      },
      error: (err) => {
        console.error(err);
        alert('Error updating device');
      },
    });
  }
}
