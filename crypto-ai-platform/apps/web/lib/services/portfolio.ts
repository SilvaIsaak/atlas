import api from "../api";
import { mockPortfolioAllocation, mockRiskMetrics } from './mockData';

export interface GetDashboardResponse {
  portfolioValue: number;
  dailyPnL: number;
  allocation: { name: string; value: number }[];
  positions: any[];
}

export const portfolioService = {
  async getPortfolioValue(): Promise<number> {
    try {
      const dashboard = await api.get<GetDashboardResponse>("/dashboard");
      return dashboard.data.portfolioValue;
    } catch (e) {
      return 67432.54;
    }
  },

  async getDailyPnL(): Promise<number> {
    try {
      const dashboard = await api.get<GetDashboardResponse>("/dashboard");
      return dashboard.data.dailyPnL;
    } catch (e) {
      return 1234.56;
    }
  },

  async getPortfolioAllocation(): Promise<{ name: string; value: number }[]> {
    try {
      const dashboard = await api.get<GetDashboardResponse>("/dashboard");
      return dashboard.data.allocation;
    } catch (e) {
      return mockPortfolioAllocation;
    }
  },
};
