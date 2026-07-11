'use client';

import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { useQuery } from '@tanstack/react-query';
import { riskService } from '@/lib/services';
import { mockRiskMetrics, mockAlerts } from '@/lib/services/mockData';
import { AlertTriangle, ShieldAlert, TrendingDown } from 'lucide-react';

export default function RiskMonitorPage() {
  const { data: riskMetrics = mockRiskMetrics } = useQuery({ 
    queryKey: ['riskMetrics'], 
    queryFn: riskService.getRiskMetrics 
  });
  const { data: alerts = mockAlerts } = useQuery({ 
    queryKey: ['riskAlerts'], 
    queryFn: riskService.getAlerts 
  });

  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <div>
          <h1 className="text-3xl font-bold">Risk Monitor</h1>
          <p className="text-muted-foreground">Monitoramento de risco</p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          {riskMetrics.map((metric) => (
            <Card key={metric.name}>
              <CardHeader className="pb-2">
                <CardTitle className="text-sm font-medium text-muted-foreground">{metric.name}</CardTitle>
              </CardHeader>
              <CardContent>
                <div className="text-3xl font-bold">{metric.value}</div>
              </CardContent>
            </Card>
          ))}
        </div>

        <Card>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <AlertTriangle className="h-5 w-5 text-orange-500" />
              Active Alerts
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-3">
              {alerts.map((alert, i) => (
                <div 
                  key={i} 
                  className={`p-4 rounded-lg border ${
                    alert.severity === 'high' ? 'border-red-200 bg-red-50 dark:bg-red-950/20' : 
                    alert.severity === 'medium' ? 'border-orange-200 bg-orange-50 dark:bg-orange-950/20' : 
                    'border-yellow-200 bg-yellow-50 dark:bg-yellow-950/20'
                  }`}
                >
                  <div className="flex items-center justify-between mb-2">
                    <p className="font-medium">{alert.title}</p>
                    <span className={`text-xs font-medium px-2 py-1 rounded ${
                      alert.severity === 'high' ? 'bg-red-100 text-red-800 dark:bg-red-900 dark:text-red-200' : 
                      alert.severity === 'medium' ? 'bg-orange-100 text-orange-800 dark:bg-orange-900 dark:text-orange-200' : 
                      'bg-yellow-100 text-yellow-800 dark:bg-yellow-900 dark:text-yellow-200'
                    }`}>
                      {alert.severity.toUpperCase()}
                    </span>
                  </div>
                  <p className="text-sm text-muted-foreground">{alert.message}</p>
                  <p className="text-xs text-muted-foreground mt-1">{alert.time}</p>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      </div>
    </ProtectedRoute>
  );
}
