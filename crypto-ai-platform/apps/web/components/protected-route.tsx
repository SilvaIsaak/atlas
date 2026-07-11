'use client';

import { useAuthStore } from '@/lib/stores/useAuthStore';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';
import { DashboardLayout } from './layout/dashboard-layout';

export function ProtectedRoute({ children }: { children: React.ReactNode }) {
  const { user } = useAuthStore();
  const router = useRouter();

  useEffect(() => {
    if (!user) {
      router.push('/login');
    }
  }, [user, router]);

  if (!user) {
    return null;
  }

  return <DashboardLayout>{children}</DashboardLayout>;
}
