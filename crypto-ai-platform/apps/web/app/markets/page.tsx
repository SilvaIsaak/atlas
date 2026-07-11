'use client';

import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { useDataStore } from '@/lib/stores/useDataStore';
import { useEffect } from 'react';
import { LineChart, Line, XAxis, YAxis, Tooltip, ResponsiveContainer } from 'recharts';
import { ArrowUp, ArrowDown } from 'lucide-react';

export default function MarketsPage() {
  const { assets, equityCurve, init } = useDataStore();

  useEffect(() => {
    init();
  }, [init]);

  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <div>
          <h1 className="text-3xl font-bold">Markets</h1>
          <p className="text-muted-foreground">Visão geral dos ativos</p>
        </div>

        <Card>
          <CardHeader>
            <CardTitle>Market Overview</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="h-80">
              <ResponsiveContainer width="100%" height="100%">
                <LineChart data={equityCurve}>
                  <XAxis 
                    dataKey="time" 
                    tickFormatter={(time) => new Date(time).toLocaleDateString()} 
                  />
                  <YAxis />
                  <Tooltip 
                    formatter={(value: number) => [`$${value.toFixed(2)}`, 'Price']} 
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

        <Card>
          <CardHeader>
            <CardTitle>Assets</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="overflow-x-auto">
              <table className="w-full">
                <thead>
                  <tr className="border-b">
                    <th className="text-left py-3 px-4 text-sm font-medium text-muted-foreground">Asset</th>
                    <th className="text-left py-3 px-4 text-sm font-medium text-muted-foreground">Price</th>
                    <th className="text-left py-3 px-4 text-sm font-medium text-muted-foreground">24h Change</th>
                    <th className="text-left py-3 px-4 text-sm font-medium text-muted-foreground">24h Volume</th>
                    <th className="text-left py-3 px-4 text-sm font-medium text-muted-foreground">Trend</th>
                  </tr>
                </thead>
                <tbody>
                  {assets.map((asset) => (
                    <tr key={asset.id} className="border-b hover:bg-accent/50">
                      <td className="py-4 px-4">
                        <div className="flex items-center gap-3">
                          <div className="h-10 w-10 bg-primary/10 rounded-full flex items-center justify-center">
                            <span className="text-xs font-bold">{asset.symbol}</span>
                          </div>
                          <div>
                            <p className="font-medium">{asset.symbol}</p>
                            <p className="text-sm text-muted-foreground">{asset.name}</p>
                          </div>
                        </div>
                      </td>
                      <td className="py-4 px-4 font-medium">
                        ${asset.price.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                      </td>
                      <td className={`py-4 px-4 font-medium ${asset.change24h >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                        {asset.change24h >= 0 ? '+' : ''}{asset.change24h.toFixed(2)}%
                      </td>
                      <td className="py-4 px-4 text-sm text-muted-foreground">
                        ${(asset.volume24h / 1e9).toFixed(2)}B
                      </td>
                      <td className="py-4 px-4">
                        {asset.change24h >= 0 ? (
                          <ArrowUp className="h-5 w-5 text-green-500" />
                        ) : (
                          <ArrowDown className="h-5 w-5 text-red-500" />
                        )}
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
