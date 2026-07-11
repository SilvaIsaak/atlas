'use client';
import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { useQuery } from '@tanstack/react-query';
import { aiService } from '@/lib/services';
export default function StrategiesPage() {
  const { data: strategies } = useQuery({ queryKey: ['strategies'], queryFn: aiService.getStrategies });
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <div className="flex items-center justify-between">
          <h1 className="text-3xl font-bold">Strategies</h1>
          <Button>Add New Strategy</Button>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {strategies?.map(strat => (
            <Card key={strat.id}>
              <CardHeader>
                <CardTitle>{strat.name}</CardTitle>
              </CardHeader>
              <CardContent>
                <p className="text-muted-foreground text-sm mb-4">{strat.description}</p>
                <div className="flex items-center justify-between">
                  <span className={`inline-flex px-2 py-1 rounded-full text-xs font-medium ${strat.status === 'active' ? 'bg-green-100 text-green-700' : 'bg-gray-100 text-gray-700'}`}>
                    {strat.status.toUpperCase()}
                  </span>
                  <span className={`font-bold ${strat.performance >=0 ? 'text-green-500' : 'text-red-500'}`}>
                    {strat.performance >=0 ? '+' : ''}{strat.performance.toFixed(2)}%
                  </span>
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      </div>
    </ProtectedRoute>
  );
}
