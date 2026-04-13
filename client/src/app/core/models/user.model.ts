export interface UserRequest {
  name: string;
  role: string;
  location: string;
}

export interface User extends UserRequest {
  id: number;
}
