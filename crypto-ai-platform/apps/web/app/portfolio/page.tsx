'use client';

import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { useQuery } from '@tanstack/react-query';
import { portfolioService, tradingService } from '@/lib/services';
import { mockPositions, mockPortfolioAllocation, mockEquityCurve } from '@/lib/services/mockData';
import { PieChart, Pie, Cell, ResponsiveContainer, AreaChart, Area, XAxis, YAxis, Tooltip } from 'recharts';

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042'];

export default function PortfolioPage() {
  const { data: portfolioValue = 67432 } = useQuery({ 
    queryKey: ['portfolioValue'], 
    queryFn: portfolioService.getPortfolioValue 
  });
  const { data: dailyPnL = 1216.27 } = useQuery({ 
    queryKey: ['dailyPnL'], 
    queryFn: portfolioService.getDailyPnL 
  });
  const { data: allocation = mockPortfolioAllocation } = useQuery({ 
    queryKey: ['allocation'], 
    queryFn: portfolioService.getPortfolioAllocation 
  });
  const { data: positions = mockPositions } = useQuery({ 
    queryKey: ['positions'], 
    queryFn: tradingService.getPositions 
  });

  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <div>
          <h1 className="text-3xl font-bold">Portfolio</h1>
          <p className="text-muted-foreground">Gerenciamento de portfólio</p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm font-medium text-muted-foreground">Total Value</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-3xl font-bold">${portfolioValue.toLocaleString()}</div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm font-medium text-muted-foreground">24h PnL</CardTitle>
            </CardHeader>
            <CardContent>
              <div className={`text-3xl font-bold ${dailyPnL >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                {dailyPnL >= 0 ? '+' : ''}${dailyPnL.toFixed(2)}
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm font-medium text-muted-foreground">Positions</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-3xl font-bold">{positions.length}</div>
            </CardContent>
          </Card>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <Card>
            <CardHeader>
              <CardTitle>Performance History</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-80">
                <ResponsiveContainer width="100%" height="100%">
                  <AreaChart data={mockEquityCurve}>
                    <XAxis 
                      dataKey="time" 
                      tickFormatter={(time) => new Date(time).toLocaleDateString()} 
                    />
                    <YAxis />
                    <Tooltip 
                      formatter={(value: number) => [`$${value.toFixed(2)}`, 'Value']} 
                    />
                    <Area 
                      type="monotone" 
                      dataKey="price" 
                      stroke="#3b82f6" 
                      fill="rgba(59, 130, 246, 0.2)" 
                    />
                  </AreaChart>
                </ResponsiveContainer>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle>Asset Allocation</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-80">
                <ResponsiveContainer width="100%" height="100%">
                  <PieChart>
                    <Pie
                      data={allocation}
                      cx="50%"
                      cy="50%"
                      labelLine={false}
                      label={({ name, percent }) => `${name} ${(percent * 100).toFixed(0)}%`}
                      outerRadius={100}
                      dataKey="value"
                    >
                      {allocation.map((entry, index) => (
                        <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                      ))}
                    </Pie>
                    <Tooltip />
                  </PieChart>
                </ResponsiveContainer>
              </div>
            </CardContent>
          </Card>
        </div>

        <Card>
          <CardHeader>
            <CardTitle>Positions</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="overflow-x-auto">
              <table className="w-full">
                <thead>
                  <tr className="border-b">
                    <th className="text-left py-3 px-4 text-sm font-medium text-muted-foreground">Asset</th>
                    <th className="text-left py-3 px-4 text-sm font-medium text-muted-foreground">Quantity</th>
                    <th className="text-left py-3 px-4 text-sm font-medium text-muted-foreground">Entry Price</th>
                    <th className="text-left py-3 px-4 text-sm font-medium text-muted-foreground">Current Price</th>
                    <th className="text-left py-3 px-4 text-sm font-medium text-muted-foreground">PnL</th>
                  </tr>
                </thead>
                <tbody>
                  {positions.map((pos) => (
                    <tr key={pos.id} className="border-b hover:bg-accent/50">
                      <td className="py-4 px-4">
                        <div className="flex items-center gap-3">
                          <div className="h-10 w-10 bg-primary/10 rounded-full flex items-center justify-center">
                            <span className="text-xs font-bold">{pos.symbol}</span>
                          </div>
                          <p className="font-medium">{pos.symbol}</p>
                        </div>
                      </td>
                      <td className="py-4 px-4">{pos.quantity.toFixed(4)}</td>
                      <td className="py-4 px-4">${pos.avgEntryPrice.toFixed(2)}</td>
                      <td className="py-4 px-4">${pos.currentPrice.toFixed(2)}</td>
                      <td className={`py-4 px-4 font-medium ${pos.pnl >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                        {pos.pnl >= 0 ? '+' : ''}${pos.pnl.toFixed(2)}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </CardContent>
        </Card>
      </div>
    </ProtectedRoute>
  );
}
