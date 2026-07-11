'use client';

import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { useQuery } from '@tanstack/react-query';
import { marketService } from '@/lib/services';

export default function AssetsPage() {
  const { data: assets } = useQuery({ queryKey: ['assets'], queryFn: marketService.getAssets });
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <h1 className="text-3xl font-bold">Assets</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          {assets?.map((asset) => (
            <Card key={asset.id}>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <span className="h-8 w-8 bg-primary/10 rounded-full flex items-center justify-center text-xs font-bold">{asset.symbol}</span>
                  {asset.name}
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="text-2xl font-bold">${asset.price.toLocaleString()}</div>
                <div className={`text-sm mt-1 ${asset.change24h >=0 ? 'text-green-500' : 'text-red-500'}`}>
                  {asset.change24h >=0 ? '+' : ''}{asset.change24h.toFixed(2)}%
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      </div>
    </ProtectedRoute>
  );
}
