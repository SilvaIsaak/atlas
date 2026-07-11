import { create } from 'zustand';
import { Asset, Position, Agent } from '@/lib/types';
import {
  mockAssets,
  mockPositions,
  mockAgents,
  mockAlerts,
  updateAsset,
  addEquityPoint,
  mockEquityCurve,
} from '@/lib/services/mockData';
import { realTimeService } from '@/lib/services/realTime';

type DataStore = {
  assets: Asset[];
  positions: Position[];
  agents: Agent[];
  alerts: typeof mockAlerts;
  equityCurve: typeof mockEquityCurve;
  portfolioValue: number;
  dailyPnL: number;
  init: () => void;
};

export const useDataStore = create<DataStore>((set, get) => ({
  assets: [...mockAssets],
  positions: [...mockPositions],
  agents: [...mockAgents],
  alerts: [...mockAlerts],
  equityCurve: [...mockEquityCurve],
  portfolioValue: 67432,
  dailyPnL: 1216.27,

  init: () => {
    // Subscribe to real-time updates
    realTimeService.connect();
    realTimeService.subscribeToMarket((update) => {
      set((state) => {
        const newAssets = state.assets.map((a) =>
          a.symbol === update.symbol
            ? { ...a, price: update.price, change24h: update.change24h }
            : a
        );
        const newPositions = state.positions.map((p) =>
          p.symbol === update.symbol
            ? {
                ...p,
                currentPrice: update.price,
                pnl: (update.price - p.avgEntryPrice) * p.quantity,
              }
            : p
        );
        // Calculate new portfolio value and PnL
        let newPortfolioValue = 0;
        let newDailyPnL = 0;
        newPositions.forEach((p) => {
          newPortfolioValue += p.currentPrice * p.quantity;
          newDailyPnL += p.pnl;
        });

        // Update equity curve with new portfolio value
        const newEquityCurve = [...state.equityCurve.slice(1), {
          time: new Date(),
          price: newPortfolioValue,
        }];

        return {
          assets: newAssets,
          positions: newPositions,
          portfolioValue: newPortfolioValue,
          dailyPnL: newDailyPnL,
          equityCurve: newEquityCurve,
        };
      });
    });

    realTimeService.subscribeToAlerts((alert) => {
      set((state) => ({
        alerts: [alert, ...state.alerts.slice(0, 4)],
      }));
    });
  },
}));
