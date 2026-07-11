import { mockAssets, mockEquityCurve } from './mockData';
import { Asset, MarketDataPoint } from '@/lib/types';

export const marketService = {
  async getAssets(): Promise<Asset[]> {
    return new Promise((resolve) => setTimeout(() => resolve(mockAssets), 500));
  },

  async getAsset(symbol: string): Promise<Asset | undefined> {
    return new Promise((resolve) => setTimeout(() => resolve(mockAssets.find(a => a.symbol === symbol)), 300));
  },

  async getMarketData(symbol: string): Promise<MarketDataPoint[]> {
    return new Promise((resolve) => setTimeout(() => resolve(mockEquityCurve), 600));
  },
};
