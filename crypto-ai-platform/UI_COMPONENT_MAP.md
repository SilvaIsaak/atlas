# UI Component Map

## Layout Components
| Component | Path | Purpose |
|-----------|------|---------|
| Sidebar | components/layout/sidebar.tsx | Navigation menu with nested sections |
| Topbar | components/layout/topbar.tsx | Header with search, notifications, user profile |
| DashboardLayout | components/layout/dashboard-layout.tsx | Combines Sidebar and Topbar for protected pages |
| ProtectedRoute | components/protected-route.tsx | Wraps protected pages, redirects if not authenticated |
| ErrorBoundary | components/error-boundary.tsx | Catches rendering errors and shows fallback UI |
| Loading | components/loading.tsx | Loading spinner component with size options |
| Skeleton | components/skeleton.tsx | Skeleton placeholder for content loading |

## UI Components (Shadcn/ui)
| Component | Path | Purpose |
|-----------|------|---------|
| Button | components/ui/button.tsx | Reusable button component |
| Card | components/ui/card.tsx | Card container with header and content |
| Input | components/ui/input.tsx | Text input component |
| Label | components/ui/label.tsx | Form label component |

## Page Components
| Page | Path | Purpose |
|------|------|---------|
| Dashboard | app/dashboard/page.tsx | Portfolio overview and key metrics |
| Markets Overview | app/markets/page.tsx | Market overview chart and asset list |
| Assets | app/markets/assets/page.tsx | Asset cards |
| Order Book | app/markets/orderbook/page.tsx | Order book placeholder |
| Trading Terminal | app/trading/page.tsx | Trading interface with buy/sell panel |
| Orders | app/trading/orders/page.tsx | List of orders |
| Positions | app/trading/positions/page.tsx | List of open positions |
| Portfolio | app/portfolio/page.tsx | Portfolio performance and allocation |
| Strategies | app/ai/strategies/page.tsx | List of AI strategies |
| Agents | app/ai/agents/page.tsx | AI agents list and management |
| Signals | app/ai/signals/page.tsx | AI generated trading signals |
| Experiments | app/research/experiments/page.tsx | Research experiments placeholder |
| Dataset | app/research/dataset/page.tsx | Datasets placeholder |
| Backtesting | app/research/backtesting/page.tsx | Backtesting placeholder |
| Risk Monitor | app/risk/monitor/page.tsx | Risk monitoring placeholder |
| Alerts | app/risk/alerts/page.tsx | Risk alerts placeholder |
| Settings | app/settings/page.tsx | User profile settings |
| Login | app/login/page.tsx | Login form |
| Register | app/register/page.tsx | Registration form |
