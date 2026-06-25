'use client';

import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { useAuthStore } from '@/lib/stores/useAuthStore';

export default function Home() {
  const { user, logout } = useAuthStore();

  return (
    <div>
      <header className="border-b">
        <div className="container mx-auto p-4 flex justify-between items-center">
          <Link href="/" className="text-2xl font-bold">
            Crypto AI Platform
          </Link>
          <nav className="flex items-center gap-4">
            {user ? (
              <>
                <span>
                  Welcome, {user.firstName} {user.lastName}
                </span>
                <Button onClick={logout}>Logout</Button>
              </>
            ) : (
              <>
                <Link href="/login">
                  <Button variant="ghost">Login</Button>
                </Link>
                <Link href="/register">
                  <Button>Register</Button>
                </Link>
              </>
            )}
          </nav>
        </div>
      </header>
      <main className="container mx-auto p-4">
        <h1 className="text-4xl font-bold mb-4">
          {user
            ? `Welcome back, ${user.firstName}!`
            : 'Crypto AI Platform'}
        </h1>
        <p className="text-muted-foreground">
          Plataforma enterprise de negociação quantitativa de criptomoedas
        </p>
      </main>
    </div>
  );
}
