'use client';
import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Button } from '@/components/ui/button';
import { useAuthStore } from '@/lib/stores/useAuthStore';
import { toast } from 'sonner';
export default function SettingsPage() {
  const { user } = useAuthStore();
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <h1 className="text-3xl font-bold">Settings</h1>
        <Card className="max-w-2xl">
          <CardHeader><CardTitle>Profile</CardTitle></CardHeader>
          <CardContent className="space-y-4">
            <div>
              <Label>Email</Label>
              <Input value={user?.email} disabled />
            </div>
            <div>
              <Label>First Name</Label>
              <Input value={user?.firstName} />
            </div>
            <div>
              <Label>Last Name</Label>
              <Input value={user?.lastName} />
            </div>
            <Button onClick={() => toast.success('Settings saved!')}>Save Changes</Button>
          </CardContent>
        </Card>
      </div>
    </ProtectedRoute>
  );
}
