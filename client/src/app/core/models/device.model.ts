export interface DeviceRequest {
  name: string;
  manufacturer: string;
  type: number;
  operatingSystem: string;
  osVersion: string;
  processor: string;
  ram: number;
  description: string;
  userId: number | null;
}

export interface Device extends DeviceRequest {
  id: number;
}