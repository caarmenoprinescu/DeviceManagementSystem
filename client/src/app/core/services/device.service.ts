import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Device, DeviceRequest } from '../models/device.model';

@Injectable({
  providedIn: 'root',
})
export class DeviceService {
  private apiUrl = 'http://localhost:5197/api/devices';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Device[]> {
    return this.http.get<Device[]>(this.apiUrl);
  }

  getById(id: number): Observable<Device> {
    return this.http.get<Device>(`${this.apiUrl}/${id}`);
  }

  create(device: DeviceRequest): Observable<Device> {
    return this.http.post<Device>(this.apiUrl, device);
  }

  update(id: number, device: DeviceRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, device);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  generateDescription(data: any): Observable<string> {
    return this.http.post(`${this.apiUrl}/generate-description`, data, { responseType: 'text' });
  }
}
