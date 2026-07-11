'use client';
import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
export default function DatasetPage() {
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <div className="flex justify-between items-center">
          <h1 className="text-3xl font-bold">Dataset</h1>
          <Button>Import Data</Button>
        </div>
        <Card>
          <CardHeader><CardTitle>Available Datasets</CardTitle></CardHeader>
          <CardContent className="text-muted-foreground">
            Placeholder - Dataset list will be displayed here
          </CardContent>
        </Card>
      </div>
    </ProtectedRoute>
  );
}
