import { mockRiskMetrics, mockAlerts } from './mockData';

export const riskService = {
  async getRiskMetrics() {
    await new Promise(resolve => setTimeout(resolve, 300));
    return mockRiskMetrics;
  },

  async getAlerts() {
    await new Promise(resolve => setTimeout(resolve, 300));
    return mockAlerts;
  },
};
