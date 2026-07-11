interface LoginRequest {
  email: string;
  password: string;
}

interface LoginResponse {
  userId: string;
  email: string;
  userName: string;
  firstName: string;
  lastName: string;
  roles: string[];
  accessToken: string;
  refreshToken: string;
}

interface RegisterRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  userName: string;
}

export const authService = {
  async login(data: LoginRequest): Promise<LoginResponse> {
    // Mock authentication for demo
    if (data.email === 'demo@cryptoai.com' && data.password === 'Demo123!') {
      return {
        userId: '1',
        email: 'demo@cryptoai.com',
        userName: 'demouser',
        firstName: 'Demo',
        lastName: 'User',
        roles: ['USER'],
        accessToken: 'mock_access_token_123',
        refreshToken: 'mock_refresh_token_456',
      };
    }
    throw new Error('Invalid credentials');
  },

  async register(data: RegisterRequest): Promise<void> {
    // Mock registration
    console.log('Registering user:', data);
  },
};
