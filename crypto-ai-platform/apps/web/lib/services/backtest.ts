import { mockBacktestResults } from './mockData';

export const backtestService = {
  async getResults() {
    await new Promise(resolve => setTimeout(resolve, 300));
    return mockBacktestResults;
  },
};
