import { Asset, Order, Position, Agent, Strategy, Signal, MarketDataPoint } from '@/lib/types';

// Mutable data for real-time updates
export let mockAssets: Asset[] = [
  { id: '1', symbol: 'BTC', name: 'Bitcoin', price: 67432.54, change24h: 2.34, volume24h: 45000000000 },
  { id: '2', symbol: 'ETH', name: 'Ethereum', price: 3521.12, change24h: -1.23, volume24h: 21000000000 },
  { id: '3', symbol: 'SOL', name: 'Solana', price: 145.32, change24h: 5.67, volume24h: 8500000000 },
  { id: '4', symbol: 'XRP', name: 'Ripple', price: 0.52, change24h: -2.11, volume24h: 1800000000 },
];

export const mockOrders: Order[] = [
  { id: '1', symbol: 'BTC', type: 'buy', quantity: 0.1, price: 67432.54, status: 'filled', createdAt: new Date(Date.now() - 86400000) },
  { id: '2', symbol: 'ETH', type: 'sell', quantity: 2, price: 3515.00, status: 'open', createdAt: new Date(Date.now() - 3600000) },
  { id: '3', symbol: 'SOL', type: 'buy', quantity: 10, price: 144.50, status: 'filled', createdAt: new Date(Date.now() - 18000000) },
];

export let mockPositions: Position[] = [
  { id: '1', symbol: 'BTC', quantity: 0.5, avgEntryPrice: 65000, currentPrice: 67432.54, pnl: 1216.27 },
  { id: '2', symbol: 'ETH', quantity: 5, avgEntryPrice: 3400, currentPrice: 3521.12, pnl: 605.60 },
  { id: '3', symbol: 'SOL', quantity: 50, avgEntryPrice: 130, currentPrice: 145.32, pnl: 766.00 },
];

export let mockAgents: Agent[] = [
  { id: 'supervisor', name: 'Supervisor Agent', type: 'supervisor', status: 'running', lastExecution: new Date(), decisions: ['BTC BUY'], logs: ['Agent initialized', 'Monitoring all agents'], confidence: 87, reason: 'Strong momentum + low risk' },
  { id: 'market', name: 'Market Agent', type: 'market', status: 'running', lastExecution: new Date(Date.now() - 60000), decisions: ['Market trend up'], logs: ['Data received', 'Processed 4 assets'], confidence: 92, reason: 'Volume increasing' },
  { id: 'risk', name: 'Risk Agent', type: 'risk', status: 'running', lastExecution: new Date(Date.now() - 120000), decisions: ['Risk score acceptable'], logs: ['Calculated portfolio risk', 'No alerts'], confidence: 85, reason: 'Diversified portfolio' },
  { id: 'strategy', name: 'Strategy Agent', type: 'strategy', status: 'running', lastExecution: new Date(Date.now() - 180000), decisions: ['SMA Crossover signal'], logs: ['Analyzed strategies', 'Signals generated'], confidence: 80, reason: 'Technical indicators align' },
  { id: 'portfolio', name: 'Portfolio Agent', type: 'portfolio', status: 'running', lastExecution: new Date(Date.now() - 300000), decisions: ['Rebalanced'], logs: ['Rebalancing complete'], confidence: 90, reason: 'Optimal allocation' },
];

export const mockStrategies: Strategy[] = [
  { id: '1', name: 'SMA Crossover', description: 'Simple Moving Average Crossover', status: 'active', performance: 12.34 },
  { id: '2', name: 'RSI Strategy', description: 'Relative Strength Index', status: 'active', performance: 8.91 },
  { id: '3', name: 'Mean Reversion', description: 'Mean Reversion Strategy', status: 'paused', performance: -2.34 },
];

export const mockSignals: Signal[] = [
  { id: '1', symbol: 'BTC', type: 'buy', strength: 0.85, timestamp: new Date(Date.now() - 1800000) },
  { id: '2', symbol: 'ETH', type: 'sell', strength: 0.65, timestamp: new Date(Date.now() - 3600000) },
  { id: '3', symbol: 'SOL', type: 'buy', strength: 0.90, timestamp: new Date(Date.now() - 7200000) },
];

export let mockEquityCurve: MarketDataPoint[] = Array.from({ length: 30 }, (_, i) => {
  const date = new Date();
  date.setDate(date.getDate() - (29 - i));
  return {
    time: date,
    price: 10000 + Math.random() * 5000 + i * 200,
  };
});

export const mockPortfolioAllocation = [
  { name: 'BTC', value: 45 },
  { name: 'ETH', value: 30 },
  { name: 'SOL', value: 15 },
  { name: 'Others', value: 10 },
];

export const mockRiskMetrics = [
  { name: 'Risk Score', value: 4.2 },
  { name: 'Exposure', value: 65 },
  { name: 'Drawdown', value: -8.4 },
  { name: 'Volatility', value: 12.5 },
];

export const mockTradingActivity = [
  { day: 'Mon', trades: 8, volume: 12000 },
  { day: 'Tue', trades: 12, volume: 18000 },
  { day: 'Wed', trades: 15, volume: 22000 },
  { day: 'Thu', trades: 10, volume: 16000 },
  { day: 'Fri', trades: 20, volume: 30000 },
];

export const mockBacktestResults = {
  return: 24.5,
  sharpe: 1.8,
  drawdown: -12.3,
  winRate: 72,
};

export let mockAlerts = [
  { id: 1, title: 'HIGH VOLATILITY DETECTED', severity: 'high', message: 'Market volatility increased significantly', time: '2 min ago' },
  { id: 2, title: 'BTC exposure above limit', severity: 'medium', message: 'BTC allocation exceeds recommended 50%', time: '15 min ago' },
];

export const updateAsset = (symbol: string, newPrice: number, newChange24h: number) => {
  const idx = mockAssets.findIndex(a => a.symbol === symbol);
  if (idx !== -1) {
    mockAssets[idx].price = newPrice;
    mockAssets[idx].change24h = newChange24h;
  }
  const posIdx = mockPositions.findIndex(p => p.symbol === symbol);
  if (posIdx !== -1) {
    const pos = mockPositions[posIdx];
    pos.currentPrice = newPrice;
    pos.pnl = (newPrice - pos.avgEntryPrice) * pos.quantity;
  }
};

export const addEquityPoint = (newPrice: number) => {
  mockEquityCurve = [...mockEquityCurve.slice(1), { time: new Date(), price: newPrice }];
};
