# Frontend Architecture

## Overview
The frontend is built on Next.js 15 using the App Router, following a modular, feature-based architecture.

## Directory Structure
```
apps/web/
├── app/                              # App Router pages and layout
│   ├── providers.tsx                 # Global providers
│   ├── globals.css                   # Global styles
│   ├── layout.tsx                    # Root layout
│   ├── page.tsx                      # Home page
│   ├── login/                        # Login page
│   ├── register/                     # Register page
│   ├── dashboard/                    # Dashboard page
│   ├── markets/                      # Markets section
│   ├── trading/                      # Trading section
│   ├── portfolio/                    # Portfolio section
│   ├── ai/                           # AI Engine section
│   ├── research/                     # Research section
│   ├── risk/                         # Risk section
│   └── settings/                     # Settings page
├── components/                       # Reusable components
│   ├── layout/                       # Layout specific components
│   └── ui/                           # Shadcn/ui components
├── lib/
│   ├── api.ts                        # Axios API client setup
│   ├── utils.ts                      # Utility functions (cn, etc.)
│   ├── types/                        # TypeScript type definitions
│   ├── services/                     # Service layer
│   └── stores/                       # Zustand stores
```

## Key Architectural Decisions

### 1. Routing
- **Next.js App Router**: Used for file-based routing
- **Protected Routes**: Higher-order component `ProtectedRoute` ensures only authenticated users access pages
- **Dynamic Routes**: Can be added later for asset-specific pages

### 2. State Management
- **Zustand**: Lightweight state management library for authentication state
- **React Query**: For server state, data caching, and background updates

### 3. API Integration
- **Axios**: HTTP client configured with base URL and interceptors for auth and error handling
- **Service Layer**: Separate service files (`lib/services/`) encapsulate API calls
- **Mock Services**: Placeholder implementations using mock data for rapid development

### 4. Styling
- **Tailwind CSS**: Utility-first CSS framework
- **Shadcn/ui**: Component library for consistent UI elements
- **Theme Provider**: Dark/light mode support using `next-themes`

### 5. Component Organization
- **Layout Components**: Reusable layout parts (Sidebar, Topbar, DashboardLayout)
- **UI Components**: Generic UI elements (Button, Card, Input)
- **Feature Components**: Will be added in future iterations

## Data Flow
1. **User Interaction**: User performs action in UI
2. **Service Call**: Component calls service function
3. **Data Fetching**: Service uses Axios or mock data
4. **State Update**: React Query or Zustand updates state
5. **Rerender**: Component updates with new data

## Security Considerations
- **Protected Routes**: Redirect unauthenticated users to login
- **JWT Storage**: Auth tokens stored in Zustand and persisted in localStorage
- **API Interceptor**: Adds auth header to outgoing requests
- **Error Boundary**: Catches rendering errors to prevent app crashes
