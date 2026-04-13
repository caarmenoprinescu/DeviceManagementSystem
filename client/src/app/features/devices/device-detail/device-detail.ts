import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeviceService } from '../../../core/services/device.service';
import { Device } from '../../../core/models/device.model';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatDivider } from '@angular/material/divider';

@Component({
  selector: 'app-device-detail',
  imports: [CommonModule, RouterModule, MatCardModule, MatDivider, MatButtonModule, MatIconModule],
  templateUrl: './device-detail.html',
  styleUrl: './device-detail.scss',
})
export class DeviceDetail {
  device$: Observable<Device>;

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
    private route: ActivatedRoute,
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.device$ = this.deviceService.getById(id);
  }

}
