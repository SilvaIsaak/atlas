import api from "../api";
import { mockRiskMetrics, mockAlerts } from './mockData';

export const riskService = {
  async getRiskMetrics() {
    try {
      const response = await api.get("/risk");
      return response.data;
    } catch (e) {
      return mockRiskMetrics;
    }
  },

  async getAlerts() {
    try {
      const response = await api.get("/risk/alerts");
      return response.data;
    } catch (e) {
      return mockAlerts;
    }
  },
};
