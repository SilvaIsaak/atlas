export interface Asset {
  id: string;
  symbol: string;
  name: string;
  price: number;
  change24h: number;
  volume24h: number;
}

export interface Order {
  id: string;
  symbol: string;
  type: 'buy' | 'sell';
  quantity: number;
  price: number;
  status: 'open' | 'filled' | 'cancelled';
  createdAt: Date;
}

export interface Position {
  id: string;
  symbol: string;
  quantity: number;
  avgEntryPrice: number;
  currentPrice: number;
  pnl: number;
}

export interface Agent {
  id: string;
  name: string;
  type: string;
  status: 'running' | 'stopped' | 'error';
  lastExecution: Date;
  decisions: string[];
  logs: string[];
  confidence?: number;
  reason?: string;
}

export interface Strategy {
  id: string;
  name: string;
  description: string;
  status: 'active' | 'paused';
  performance: number;
}

export interface Signal {
  id: string;
  symbol: string;
  type: 'buy' | 'sell';
  strength: number;
  timestamp: Date;
}

export interface MarketDataPoint {
  time: Date;
  price: number;
}
