import { Routes } from '@angular/router';
import { DeviceList } from './features/devices/device-list/device-list';
import { DeviceForm } from './features/devices/device-form/device-form';
import { DeviceDetail } from './features/devices/device-detail/device-detail';
import { Register } from './features/auth/register/register';
import { Login } from './features/auth/login/login';
import { UserList } from './features/users/user-list/user-list';
import { UserDetail } from './features/users/user-detail/user-detail';
import { UserForm } from './features/users/user-form/user-form';
export const routes: Routes = [
  { path: '', redirectTo: 'devices', pathMatch: 'full' },
  { path: 'devices', component: DeviceList },
  { path: 'devices/new', component: DeviceForm },
  { path: 'devices/:id', component: DeviceDetail },
  { path: 'devices/:id/edit', component: DeviceForm },
  { path: 'users', component: UserList },
{ path: 'users/new', component: UserForm },
{ path: 'users/:id/edit', component: UserForm },
{ path: 'users/:id', component: UserDetail },
  { path: 'auth/login', component: Login },
  { path: 'auth/register', component: Register },
];
