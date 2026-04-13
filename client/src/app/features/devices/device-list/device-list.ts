import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../core/services/user.service';
import { DeviceService } from '../../../core/services/device.service';
import { Device, DeviceRequest } from '../../../core/models/device.model';
import { User } from '../../../core/models/user.model';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
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
    'assignToMe',
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

  assignToMe(device: Device): void {
    device.userId = Number(localStorage.getItem('userId'));
    const deviceReq: DeviceRequest = {
      name: device.name,
      manufacturer: device.manufacturer,
      type: device.type,
      operatingSystem: device.operatingSystem,
      osVersion: device.osVersion,
      processor: device.processor,
      ram: device.ram,
      description: device.description,
      userId: device.userId,
    };
    this.deviceService.update(device.id, deviceReq).subscribe(() => {
      this.devices$ = this.deviceService.getAll();
    });
  }

  unassignFromMe(device: Device): void {
    device.userId =null;
    const deviceReq: DeviceRequest = {
      name: device.name,
      manufacturer: device.manufacturer,
      type: device.type,
      operatingSystem: device.operatingSystem,
      osVersion: device.osVersion,
      processor: device.processor,
      ram: device.ram,
      description: device.description,
      userId: device.userId,
    };
    this.deviceService.update(device.id, deviceReq).subscribe(() => {
      this.devices$ = this.deviceService.getAll();
    });
  }
  onAction(device: Device):void{
    if(device.userId == null || device.userId == 0 )   
      this.assignToMe(device);
    else if(device.userId == Number(localStorage.getItem("userId")))
      this.unassignFromMe(device);
  }

  isDisabled(device:Device):boolean{
    if(device.userId == 0)
      return false;
    if(device.userId != null && device.userId != Number(localStorage.getItem("userId")))
      return true;
    
    return false;
  }
getIcon(device:Device): string
{
   if(device.userId != Number(localStorage.getItem("userId")))   
      return "add";
    else return "remove";
}}