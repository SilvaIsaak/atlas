'use client';

import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { PieChart, Pie, Cell, ResponsiveContainer, AreaChart, Area, XAxis, YAxis, Tooltip, BarChart, Bar, Legend } from 'recharts';
import { useDataStore } from '@/lib/stores/useDataStore';
import { useEffect } from 'react';
import {
  DollarSign,
  TrendingUp,
  TrendingDown,
  Activity,
  ShieldAlert,
  Briefcase,
  User,
  Zap,
  ArrowUpRight,
} from 'lucide-react';
import { mockRiskMetrics, mockTradingActivity, mockPortfolioAllocation } from '@/lib/services/mockData';

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042'];

export default function DashboardPage() {
  const {
    assets,
    portfolioValue,
    dailyPnL,
    equityCurve,
    alerts,
    agents,
    init,
  } = useDataStore();

  useEffect(() => {
    init();
  }, [init]);

  const btc = assets.find(a => a.symbol === 'BTC');
  const eth = assets.find(a => a.symbol === 'ETH');

  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <div>
          <h1 className="text-3xl font-bold">Dashboard</h1>
          <p className="text-muted-foreground">Executive overview of your portfolio</p>
        </div>

        {/* Market Widgets */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm text-muted-foreground flex items-center gap-2">
                <div className="w-6 h-6 bg-orange-100 dark:bg-orange-950 rounded-full flex items-center justify-center">
                  <span className="text-xs font-bold">₿</span>
                </div>
                Bitcoin (BTC)
              </CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-2xl font-bold">
                ${btc?.price.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
              </div>
              <div className={`text-sm ${btc?.change24h >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                {btc?.change24h >= 0 ? '+' : ''}{btc?.change24h.toFixed(2)}%
                <TrendingUp className="inline w-4 h-4 ml-1" />
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm text-muted-foreground flex items-center gap-2">
                <div className="w-6 h-6 bg-blue-100 dark:bg-blue-950 rounded-full flex items-center justify-center">
                  <span className="text-xs font-bold">Ξ</span>
                </div>
                Ethereum (ETH)
              </CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-2xl font-bold">
                ${eth?.price.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
              </div>
              <div className={`text-sm ${eth?.change24h >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                {eth?.change24h >= 0 ? '+' : ''}{eth?.change24h.toFixed(2)}%
                {eth?.change24h >= 0 ? (
                  <TrendingUp className="inline w-4 h-4 ml-1" />
                ) : (
                  <TrendingDown className="inline w-4 h-4 ml-1" />
                )}
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm text-muted-foreground">AI Status</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="flex items-center justify-between">
                <span className="text-sm text-muted-foreground">Agents Active</span>
                <span className="font-bold">{agents.filter(a => a.status === 'running').length}</span>
              </div>
              <div className="flex items-center justify-between mt-2">
                <span className="text-sm text-muted-foreground">Signals Today</span>
                <span className="font-bold">12</span>
              </div>
              <div className="flex items-center justify-between mt-2">
                <span className="text-sm text-muted-foreground">Opportunities</span>
                <span className="font-bold text-green-500">3</span>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm text-muted-foreground">Portfolio Value</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-2xl font-bold">
                ${portfolioValue.toLocaleString(undefined, { minimumFractionDigits: 2 })}
              </div>
              <div className={`text-sm ${dailyPnL >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                {dailyPnL >= 0 ? '+' : ''}${dailyPnL.toFixed(2)} (24h)
              </div>
            </CardContent>
          </Card>
        </div>

        {/* Charts */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          <Card className="lg:col-span-2">
            <CardHeader>
              <CardTitle>Performance History</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-80">
                <ResponsiveContainer width="100%" height="100%">
                  <AreaChart data={equityCurve}>
                    <XAxis
                      dataKey="time"
                      tickFormatter={(time) => new Date(time).toLocaleDateString()}
                    />
                    <YAxis />
                    <Tooltip
                      formatter={(value: number) => [`$${value.toFixed(2)}`, 'Value']}
                      labelFormatter={(time) => new Date(time).toLocaleDateString()}
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
                      data={mockPortfolioAllocation}
                      cx="50%"
                      cy="50%"
                      labelLine={false}
                      label={({ name, percent }) => `${name} ${(percent * 100).toFixed(0)}%`}
                      outerRadius={80}
                      dataKey="value"
                    >
                      {mockPortfolioAllocation.map((entry, index) => (
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

        {/* Trading & Risk Widgets */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          <Card>
            <CardHeader>
              <CardTitle>Trading Activity</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-80">
                <ResponsiveContainer width="100%" height="100%">
                  <BarChart data={mockTradingActivity}>
                    <XAxis dataKey="day" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Bar dataKey="trades" fill="#8884d8" />
                    <Bar dataKey="volume" fill="#82ca9d" />
                  </BarChart>
                </ResponsiveContainer>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle>Risk Metrics</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-3">
                {mockRiskMetrics.map((metric) => (
                  <div key={metric.name} className="flex justify-between items-center border-b pb-2 last:border-0">
                    <span className="text-sm text-muted-foreground">{metric.name}</span>
                    <span className={`font-bold ${metric.name.includes('Drawdown') ? 'text-red-500' : ''}`}>
                      {typeof metric.value === 'number' && metric.value.toFixed(1)}
                    </span>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle>Active Alerts</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-3">
                {alerts.slice(0, 4).map((alert) => (
                  <div
                    key={alert.id}
                    className={`p-3 rounded-lg border ${
                      alert.severity === 'high'
                        ? 'border-red-200 bg-red-50 dark:bg-red-950/20'
                        : alert.severity === 'medium'
                        ? 'border-orange-200 bg-orange-50 dark:bg-orange-950/20'
                        : 'border-yellow-200 bg-yellow-50 dark:bg-yellow-950/20'
                    }`}
                  >
                    <p className="text-sm font-semibold">{alert.title}</p>
                    <p className="text-xs text-muted-foreground mt-1">{alert.message}</p>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>
        </div>
      </div>
    </ProtectedRoute>
  );
}
