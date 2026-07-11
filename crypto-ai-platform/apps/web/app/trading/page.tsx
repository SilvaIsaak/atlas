'use client';

import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Switch } from '@/components/ui/switch';
import { useState } from 'react';
import { toast } from 'sonner';
import { useDataStore } from '@/lib/stores/useDataStore';
import { useEffect } from 'react';
import { LineChart, Line, XAxis, YAxis, Tooltip, ResponsiveContainer } from 'recharts';

export default function TradingPage() {
  const [symbol, setSymbol] = useState('BTC');
  const [quantity, setQuantity] = useState('0.1');
  const [orderType, setOrderType] = useState<'buy' | 'sell'>('buy');
  const [paperTrading, setPaperTrading] = useState(true);
  const { positions, equityCurve, assets, init } = useDataStore();

  useEffect(() => {
    init();
  }, [init]);

  const selectedAsset = assets.find(a => a.symbol === symbol);

  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <div className="flex items-center justify-between">
          <h1 className="text-3xl font-bold">Trading Terminal</h1>
          <div className="flex items-center gap-2">
            <span className="text-sm text-muted-foreground">Paper Trading</span>
            <Switch 
              checked={paperTrading} 
              onCheckedChange={setPaperTrading} 
            />
          </div>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          <Card className="lg:col-span-2">
            <CardHeader>
              <CardTitle>{symbol} / USD</CardTitle>
              {selectedAsset && (
                <p className="text-2xl font-bold mt-2">
                  ${selectedAsset.price.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                  <span className={`text-sm ml-2 ${selectedAsset.change24h >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                    {selectedAsset.change24h >= 0 ? '+' : ''}{selectedAsset.change24h.toFixed(2)}%
                  </span>
                </p>
              )}
            </CardHeader>
            <CardContent>
              <div className="h-96">
                <ResponsiveContainer width="100%" height="100%">
                  <LineChart data={equityCurve}>
                    <XAxis 
                      dataKey="time" 
                      tickFormatter={(time) => new Date(time).toLocaleTimeString([], {hour: '2-digit', minute: '2-digit'})} 
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
            <CardHeader><CardTitle>Order</CardTitle></CardHeader>
            <CardContent className="space-y-4">
              <div className="flex gap-2">
                <Button 
                  variant={orderType === 'buy' ? 'default' : 'secondary'} 
                  onClick={() => setOrderType('buy')} 
                  className="flex-1"
                >
                  Buy
                </Button>
                <Button 
                  variant={orderType === 'sell' ? 'default' : 'secondary'} 
                  onClick={() => setOrderType('sell')} 
                  className="flex-1"
                >
                  Sell
                </Button>
              </div>
              <div>
                <label className="text-sm font-medium mb-2 block">Quantity</label>
                <Input type="number" value={quantity} onChange={(e) => setQuantity(e.target.value)} />
              </div>
              <Button 
                className="w-full" 
                onClick={() => {
                  toast.success(`${orderType.toUpperCase()} order placed for ${quantity} ${symbol}!`);
                }}
              >
                {orderType === 'buy' ? 'Buy' : 'Sell'} {symbol}
              </Button>
            </CardContent>
          </Card>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <Card>
            <CardHeader>
              <CardTitle>Open Positions</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-2">
                {positions.map((pos) => (
                  <div key={pos.id} className="flex justify-between items-center py-2 border-b last:border-0">
                    <div className="flex items-center gap-3">
                      <div className="h-8 w-8 bg-primary/10 rounded-full flex items-center justify-center">
                        <span className="text-xs font-bold">{pos.symbol}</span>
                      </div>
                      <div>
                        <p className="font-medium">{pos.symbol}</p>
                        <p className="text-sm text-muted-foreground">{pos.quantity.toFixed(4)}</p>
                      </div>
                    </div>
                    <div className="text-right">
                      <p className="font-medium">${(pos.currentPrice * pos.quantity).toFixed(2)}</p>
                      <p className={`text-sm ${pos.pnl >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                        {pos.pnl >= 0 ? '+' : ''}{pos.pnl.toFixed(2)}
                      </p>
                    </div>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle>Order History</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-2">
                <div className="flex justify-between items-center py-2 border-b">
                  <div>
                    <p className="font-medium">BTC BUY</p>
                    <p className="text-sm text-muted-foreground">0.1 BTC @ $67,432</p>
                  </div>
                  <p className="text-sm text-muted-foreground">2h ago</p>
                </div>
                <div className="flex justify-between items-center py-2 border-b">
                  <div>
                    <p className="font-medium">ETH BUY</p>
                    <p className="text-sm text-muted-foreground">0.5 ETH @ $3,589</p>
                  </div>
                  <p className="text-sm text-muted-foreground">4h ago</p>
                </div>
                <div className="flex justify-between items-center py-2">
                  <div>
                    <p className="font-medium">SOL SELL</p>
                    <p className="text-sm text-muted-foreground">5 SOL @ $172</p>
                  </div>
                  <p className="text-sm text-muted-foreground">6h ago</p>
                </div>
              </div>
            </CardContent>
          </Card>
        </div>
      </div>
    </ProtectedRoute>
  );
}
