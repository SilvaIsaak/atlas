'use client';

import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { useState } from 'react';
import { toast } from 'sonner';
import { useQuery } from '@tanstack/react-query';
import { backtestService } from '@/lib/services';
import { mockBacktestResults, mockEquityCurve } from '@/lib/services/mockData';
import { LineChart, Line, XAxis, YAxis, Tooltip, ResponsiveContainer } from 'recharts';

export default function BacktestingPage() {
  const [asset, setAsset] = useState('BTC');
  const [strategy, setStrategy] = useState('momentum');
  const [period, setPeriod] = useState('30d');
  const [capital, setCapital] = useState('10000');
  const [results, setResults] = useState<typeof mockBacktestResults | null>(null);
  const { data: defaultResults = mockBacktestResults } = useQuery({ 
    queryKey: ['backtestResults'], 
    queryFn: backtestService.getResults 
  });

  const handleRunBacktest = () => {
    setResults(defaultResults);
    toast.success('Backtest completed!');
  };

  const displayResults = results || defaultResults;

  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <div className="flex justify-between items-center">
          <h1 className="text-3xl font-bold">Backtesting</h1>
          <Button onClick={handleRunBacktest}>Run Backtest</Button>
        </div>

        <Card>
          <CardHeader>
            <CardTitle>Parameters</CardTitle>
          </CardHeader>
          <CardContent className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
            <div>
              <label className="text-sm font-medium mb-2 block">Asset</label>
              <Select value={asset} onValueChange={setAsset}>
                <SelectTrigger>
                  <SelectValue />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="BTC">BTC/USD</SelectItem>
                  <SelectItem value="ETH">ETH/USD</SelectItem>
                  <SelectItem value="SOL">SOL/USD</SelectItem>
                  <SelectItem value="XRP">XRP/USD</SelectItem>
                </SelectContent>
              </Select>
            </div>

            <div>
              <label className="text-sm font-medium mb-2 block">Strategy</label>
              <Select value={strategy} onValueChange={setStrategy}>
                <SelectTrigger>
                  <SelectValue />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="momentum">Momentum</SelectItem>
                  <SelectItem value="mean-reversion">Mean Reversion</SelectItem>
                  <SelectItem value="ai-agent">AI Agent</SelectItem>
                </SelectContent>
              </Select>
            </div>

            <div>
              <label className="text-sm font-medium mb-2 block">Period</label>
              <Select value={period} onValueChange={setPeriod}>
                <SelectTrigger>
                  <SelectValue />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="7d">7 Days</SelectItem>
                  <SelectItem value="30d">30 Days</SelectItem>
                  <SelectItem value="90d">90 Days</SelectItem>
                </SelectContent>
              </Select>
            </div>

            <div>
              <label className="text-sm font-medium mb-2 block">Capital</label>
              <Input type="number" value={capital} onChange={(e) => setCapital(e.target.value)} />
            </div>
          </CardContent>
        </Card>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm font-medium text-muted-foreground">Return</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-3xl font-bold text-green-500">+{displayResults.return}%</div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm font-medium text-muted-foreground">Sharpe Ratio</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-3xl font-bold">{displayResults.sharpe}</div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm font-medium text-muted-foreground">Max Drawdown</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-3xl font-bold text-red-500">{displayResults.drawdown}%</div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm font-medium text-muted-foreground">Win Rate</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-3xl font-bold">{displayResults.winRate}%</div>
            </CardContent>
          </Card>
        </div>

        <Card>
          <CardHeader>
            <CardTitle>Performance Curve</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="h-80">
              <ResponsiveContainer width="100%" height="100%">
                <LineChart data={mockEquityCurve}>
                  <XAxis 
                    dataKey="time" 
                    tickFormatter={(time) => new Date(time).toLocaleDateString()} 
                  />
                  <YAxis />
                  <Tooltip 
                    formatter={(value: number) => [`$${value.toFixed(2)}`, 'Value']} 
                  />
                  <Line 
                    type="monotone" 
                    dataKey="price" 
                    stroke="#3b82f6" 
                    strokeWidth={2} 
                  />
                </LineChart>
              </ResponsiveContainer>
            </div>
          </CardContent>
        </Card>
      </div>
    </ProtectedRoute>
  );
}
