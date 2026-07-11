import { mockAssets, mockPositions, mockAgents, mockAlerts } from './mockData';

export type MarketUpdate = {
  symbol: string;
  price: number;
  change24h: number;
  timestamp: Date;
};

export type AgentUpdate = {
  agentId: string;
  status: string;
  lastDecision?: string;
  confidence?: number;
  timestamp: Date;
};

export type AlertUpdate = {
  id: number;
  title: string;
  severity: string;
  message: string;
  time: string;
};

class RealTimeMockService {
  private subscribers: {
    market: ((update: MarketUpdate) => void)[];
    agent: ((update: AgentUpdate) => void)[];
    alert: ((update: AlertUpdate) => void)[];
  } = {
    market: [],
    agent: [],
    alert: [],
  };

  private isRunning = false;

  connect() {
    if (this.isRunning) return;
    this.isRunning = true;
    
    // Simulate market updates every 2 seconds
    setInterval(() => {
      const asset = mockAssets[Math.floor(Math.random() * mockAssets.length)];
      const priceChange = (Math.random() - 0.5) * 0.02; // ±1%
      const newPrice = asset.price * (1 + priceChange);
      const newChange24h = asset.change24h + priceChange * 100;

      this.subscribers.market.forEach((cb) => {
        cb({
          symbol: asset.symbol,
          price: parseFloat(newPrice.toFixed(2)),
          change24h: parseFloat(newChange24h.toFixed(2)),
          timestamp: new Date(),
        });
      });
    }, 2000);

    // Simulate agent updates every 5 seconds
    setInterval(() => {
      const agent = mockAgents[Math.floor(Math.random() * mockAgents.length)];
      this.subscribers.agent.forEach((cb) => {
        cb({
          agentId: agent.id,
          status: agent.status,
          lastDecision: agent.decisions[0],
          confidence: agent.confidence,
          timestamp: new Date(),
        });
      });
    }, 5000);

    // Simulate occasional alerts
    setInterval(() => {
      if (Math.random() > 0.7) {
        const alert = mockAlerts[Math.floor(Math.random() * mockAlerts.length)];
        this.subscribers.alert.forEach((cb) => {
          cb({
            ...alert,
            time: 'Just now',
          });
        });
      }
    }, 10000);
  }

  subscribeToMarket(callback: (update: MarketUpdate) => void) {
    this.subscribers.market.push(callback);
    return () => {
      this.subscribers.market = this.subscribers.market.filter((cb) => cb !== callback);
    };
  }

  subscribeToAgents(callback: (update: AgentUpdate) => void) {
    this.subscribers.agent.push(callback);
    return () => {
      this.subscribers.agent = this.subscribers.agent.filter((cb) => cb !== callback);
    };
  }

  subscribeToAlerts(callback: (update: AlertUpdate) => void) {
    this.subscribers.alert.push(callback);
    return () => {
      this.subscribers.alert = this.subscribers.alert.filter((cb) => cb !== callback);
    };
  }
}

export const realTimeService = new RealTimeMockService();
