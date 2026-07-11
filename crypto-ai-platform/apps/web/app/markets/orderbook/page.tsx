'use client';
import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
export default function OrderBookPage() {
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <h1 className="text-3xl font-bold">Order Book</h1>
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <Card>
            <CardHeader><CardTitle>Bids</CardTitle></CardHeader>
            <CardContent>
              <div className="space-y-2">
                {Array.from({length:10}, (_,i) => (
                  <div key={i} className="flex justify-between text-sm">
                    <span className="text-green-500">{(67000 - i*50).toFixed(2)}</span>
                    <span className="text-muted-foreground">{(Math.random() * 0.5).toFixed(4)}</span>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>
          <Card>
            <CardHeader><CardTitle>Asks</CardTitle></CardHeader>
            <CardContent>
              <div className="space-y-2">
                {Array.from({length:10}, (_,i) => (
                  <div key={i} className="flex justify-between text-sm">
                    <span className="text-red-500">{(67500 + i*50).toFixed(2)}</span>
                    <span className="text-muted-foreground">{(Math.random() * 0.5).toFixed(4)}</span>
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
