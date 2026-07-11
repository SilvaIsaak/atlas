'use client';
import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { useQuery } from '@tanstack/react-query';
import { aiService } from '@/lib/services';
export default function SignalsPage() {
  const { data: signals } = useQuery({ queryKey: ['signals'], queryFn: aiService.getSignals });
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <h1 className="text-3xl font-bold">Signals</h1>
        <div className="space-y-4">
          {signals?.map(signal => (
            <Card key={signal.id}>
              <CardHeader className="flex flex-row justify-between items-center">
                <div>
                  <CardTitle className="flex items-center gap-3">
                    <span className="h-10 w-10 bg-primary/10 rounded-full flex items-center justify-center text-xs font-bold">{signal.symbol}</span>
                    {signal.symbol}
                  </CardTitle>
                  <p className="text-sm text-muted-foreground">{signal.timestamp.toLocaleString()}</p>
                </div>
                <div className="text-right">
                  <span className={`text-lg font-bold ${signal.type === 'buy' ? 'text-green-500' : 'text-red-500'}`}>
                    {signal.type.toUpperCase()}
                  </span>
                  <p className="text-sm text-muted-foreground">Strength: {(signal.strength *100).toFixed(0)}%</p>
                </div>
              </CardHeader>
            </Card>
          ))}
        </div>
      </div>
    </ProtectedRoute>
  );
}
