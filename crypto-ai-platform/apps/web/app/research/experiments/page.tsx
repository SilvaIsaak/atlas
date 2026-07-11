'use client';
import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
export default function ExperimentsPage() {
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <div className="flex justify-between items-center">
          <h1 className="text-3xl font-bold">Experiments</h1>
          <Button>New Experiment</Button>
        </div>
        <Card>
          <CardHeader><CardTitle>Recent Experiments</CardTitle></CardHeader>
          <CardContent className="text-muted-foreground">
            Placeholder - Experiments will be displayed here
          </CardContent>
        </Card>
      </div>
    </ProtectedRoute>
  );
}
