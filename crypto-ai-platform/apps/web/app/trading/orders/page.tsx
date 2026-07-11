'use client';
import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { useQuery } from '@tanstack/react-query';
import { tradingService } from '@/lib/services';
export default function OrdersPage() {
  const { data: orders } = useQuery({ queryKey: ['orders'], queryFn: tradingService.getOrders });
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <h1 className="text-3xl font-bold">Orders</h1>
        <Card>
          <CardHeader><CardTitle>All Orders</CardTitle></CardHeader>
          <CardContent>
            <table className="w-full">
              <thead>
                <tr className="border-b">
                  <th className="text-left py-3 px-4 text-sm text-muted-foreground">Symbol</th>
                  <th className="text-left py-3 px-4 text-sm text-muted-foreground">Type</th>
                  <th className="text-left py-3 px-4 text-sm text-muted-foreground">Quantity</th>
                  <th className="text-left py-3 px-4 text-sm text-muted-foreground">Price</th>
                  <th className="text-left py-3 px-4 text-sm text-muted-foreground">Status</th>
                </tr>
              </thead>
              <tbody>
                {orders?.map(order => (
                  <tr key={order.id} className="border-b">
                    <td className="py-3 px-4">{order.symbol}</td>
                    <td className={`py-3 px-4 ${order.type === 'buy' ? 'text-green-500' : 'text-red-500'}`}>{order.type.toUpperCase()}</td>
                    <td className="py-3 px-4">{order.quantity}</td>
                    <td className="py-3 px-4">${order.price.toFixed(2)}</td>
                    <td className="py-3 px-4 capitalize">{order.status}</td>
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
