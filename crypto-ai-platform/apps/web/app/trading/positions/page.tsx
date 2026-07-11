'use client';
import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { useQuery } from '@tanstack/react-query';
import { tradingService } from '@/lib/services';
export default function PositionsPage() {
  const { data: positions } = useQuery({ queryKey: ['positions'], queryFn: tradingService.getPositions });
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <h1 className="text-3xl font-bold">Positions</h1>
        <Card>
          <CardHeader><CardTitle>Open Positions</CardTitle></CardHeader>
          <CardContent>
            <table className="w-full">
              <thead>
                <tr className="border-b">
                  <th className="text-left py-3 px-4 text-sm text-muted-foreground">Symbol</th>
                  <th className="text-left py-3 px-4 text-sm text-muted-foreground">Quantity</th>
                  <th className="text-left py-3 px-4 text-sm text-muted-foreground">Avg Entry</th>
                  <th className="text-left py-3 px-4 text-sm text-muted-foreground">Current</th>
                  <th className="text-left py-3 px-4 text-sm text-muted-foreground">PnL</th>
                </tr>
              </thead>
              <tbody>
                {positions?.map(pos => (
                  <tr key={pos.id} className="border-b">
                    <td className="py-3 px-4">{pos.symbol}</td>
                    <td className="py-3 px-4">{pos.quantity.toFixed(4)}</td>
                    <td className="py-3 px-4">${pos.avgEntryPrice.toFixed(2)}</td>
                    <td className="py-3 px-4">${pos.currentPrice.toFixed(2)}</td>
                    <td className={`py-3 px-4 font-medium ${pos.pnl >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                      {pos.pnl >=0 ? '+' : ''}{pos.pnl.toFixed(2)}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </CardContent>
        </Card>
      </div>
    </ProtectedRoute>
  );
}
