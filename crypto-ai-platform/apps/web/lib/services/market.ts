import api from "../api";
import { Asset, MarketDataPoint } from '@/lib/types';

export interface ExchangeTicker {
  symbol: string;
  price: number;
  priceChangePercent: number;
  volume: number;
}

export interface ExchangeKline {
  openTime: Date;
  open: number;
  high: number;
  low: number;
  close: number;
  volume: number;
}

export const marketService = {
  async getAssets(): Promise<Asset[]> {
    // For now, we'll use mock, but we can connect to /market/ticker later
    const symbols = ['BTCUSDT', 'ETHUSDT', 'SOLUSDT', 'XRPUSDT'];
    const assets: Asset[] = [];
    for (const symbol of symbols) {
      try {
        const ticker = await api.get<ExchangeTicker>('/market/ticker', {
          params: { exchangeCode: 'binance', symbol },
        });
        assets.push({
          id: symbol,
          symbol: symbol.replace('USDT', ''),
          name: symbol.replace('USDT', ''),
          price: ticker.data.price,
          change24h: ticker.data.priceChangePercent,
          volume24h: ticker.data.volume,
        });
      } catch (e) {
        // Fallback to mock if API fails
      }
    }
    return assets;
  },

  async getAsset(symbol: string): Promise<Asset | undefined> {
    const assets = await this.getAssets();
    return assets.find(a => a.symbol === symbol);
  },

  async getMarketData(symbol: string): Promise<MarketDataPoint[]> {
    try {
      const klines = await api.get<ExchangeKline[]>('/market/klines', {
        params: { exchangeCode: 'binance', symbol: `${symbol}USDT`, interval: '1d' },
      });
      return klines.data.map(k => ({
        time: k.openTime,
        price: k.close,
      }));
    } catch (e) {
      // Fallback to mock if API fails
      const { mockEquityCurve } = await import('./mockData');
      return mockEquityCurve;
    }
  },
};
