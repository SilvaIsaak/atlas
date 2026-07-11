'use client';
import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
export default function AlertsPage() {
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <h1 className="text-3xl font-bold">Alerts</h1>
        <Card>
          <CardHeader><CardTitle>Active Alerts</CardTitle></CardHeader>
          <CardContent className="text-muted-foreground">
            Placeholder - Alerts will be displayed here
          </CardContent>
        </Card>
      </div>
    </ProtectedRoute>
  );
}
