import { mockPortfolioAllocation, mockRiskMetrics } from './mockData';

export const portfolioService = {
  async getPortfolioValue(): Promise<number> {
    return new Promise((resolve) => setTimeout(() => resolve(67432.54), 300));
  },

  async getDailyPnL(): Promise<number> {
    return new Promise((resolve) => setTimeout(() => resolve(1234.56), 300));
  },

  async getPortfolioAllocation(): Promise<{ name: string; value: number }[]> {
    return new Promise((resolve) => setTimeout(() => resolve(mockPortfolioAllocation), 400));
  },

  async getRiskMetrics(): Promise<{ name: string; value: number }[]> {
    return new Promise((resolve) => setTimeout(() => resolve(mockRiskMetrics), 400));
  },
};
