import { Routes } from '@angular/router';
import { DeviceList } from './features/devices/device-list/device-list';
import { DeviceForm } from './features/devices/device-form/device-form';
import { DeviceDetail } from './features/devices/device-detail/device-detail';
export const routes: Routes = [
  { path: '', redirectTo: 'devices', pathMatch: 'full' },
  { path: 'devices', component: DeviceList },
  { path: 'devices/new', component: DeviceForm },
  { path: 'devices/:id', component: DeviceDetail },

  { path: 'devices/:id/edit', component: DeviceForm },
];
