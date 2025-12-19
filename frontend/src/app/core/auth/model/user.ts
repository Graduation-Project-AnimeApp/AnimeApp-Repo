export interface User {
  id: number;
  username: string;
  email: string;
}

export interface AuthResponse {
  user: {
    id: number;
    email: string;
    username: string;
  };
}
