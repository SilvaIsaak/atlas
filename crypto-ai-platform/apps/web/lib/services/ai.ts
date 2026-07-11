import { mockAgents, mockStrategies, mockSignals } from './mockData';
import { Agent, Strategy, Signal } from '@/lib/types';

export const aiService = {
  async getAgents(): Promise<Agent[]> {
    return new Promise((resolve) => setTimeout(() => resolve(mockAgents), 500));
  },

  async getAgent(id: string): Promise<Agent | undefined> {
    return new Promise((resolve) => setTimeout(() => resolve(mockAgents.find(a => a.id === id)), 300));
  },

  async getStrategies(): Promise<Strategy[]> {
    return new Promise((resolve) => setTimeout(() => resolve(mockStrategies), 400));
  },

  async getSignals(): Promise<Signal[]> {
    return new Promise((resolve) => setTimeout(() => resolve(mockSignals), 400));
  },
};
