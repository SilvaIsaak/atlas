import { mockOrders, mockPositions } from './mockData';
import { Order, Position } from '@/lib/types';

export const tradingService = {
  async getOrders(): Promise<Order[]> {
    return new Promise((resolve) => setTimeout(() => resolve(mockOrders), 400));
  },

  async getPositions(): Promise<Position[]> {
    return new Promise((resolve) => setTimeout(() => resolve(mockPositions), 400));
  },

  async placeOrder(order: Omit<Order, 'id' | 'createdAt' | 'status'>): Promise<Order> {
    return new Promise((resolve) => setTimeout(() => {
      const newOrder: Order = {
        ...order,
        id: Date.now().toString(),
        status: 'open',
        createdAt: new Date(),
      };
      resolve(newOrder);
    }, 800));
  },
};
