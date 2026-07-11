'use client';

import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { useAuthStore } from '@/lib/stores/useAuthStore';
import { Brain, TrendingUp, ShieldAlert, BarChart3, ArrowRight } from 'lucide-react';

export default function Home() {
  const { user, logout } = useAuthStore();

  const features = [
    {
      icon: Brain,
      title: 'AI-Powered Trading',
      description: 'Autonomous agents analyze markets and execute trades 24/7 with high precision.',
    },
    {
      icon: TrendingUp,
      title: 'Professional Trading',
      description: 'Advanced trading terminal with paper trading, order management, and real-time charts.',
    },
    {
      icon: ShieldAlert,
      title: 'Risk Management',
      description: 'Comprehensive risk monitoring, alerts, and drawdown controls to protect your portfolio.',
    },
    {
      icon: BarChart3,
      title: 'Advanced Analytics',
      description: 'Backtesting, portfolio analytics, and market microstructure analysis.',
    },
  ];

  if (user) {
    return (
      <div className="min-h-screen bg-background">
        <header className="border-b">
          <div className="container mx-auto p-4 flex justify-between items-center">
            <Link href="/" className="text-2xl font-bold">
              Crypto AI Platform
            </Link>
            <nav className="flex items-center gap-4">
              <span>Welcome, {user.firstName} {user.lastName}</span>
              <Link href="/dashboard">
                <Button>Dashboard</Button>
              </Link>
              <Button variant="ghost" onClick={logout}>Logout</Button>
            </nav>
          </div>
        </header>
        <main className="container mx-auto p-8">
          <div className="text-center max-w-3xl mx-auto">
            <h1 className="text-4xl font-bold mb-4">
              Welcome back, {user.firstName}!
            </h1>
            <p className="text-muted-foreground mb-8">
              Ready to continue your trading journey?
            </p>
            <Link href="/dashboard">
              <Button size="lg">
                Go to Dashboard
                <ArrowRight className="ml-2 h-4 w-4" />
              </Button>
            </Link>
          </div>
        </main>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-background">
      <header className="border-b">
        <div className="container mx-auto p-4 flex justify-between items-center">
          <Link href="/" className="text-2xl font-bold">
            Crypto AI Platform
          </Link>
          <nav className="flex items-center gap-4">
            <Link href="/login">
              <Button variant="ghost">Login</Button>
            </Link>
            <Link href="/register">
              <Button>Register</Button>
            </Link>
          </nav>
        </div>
      </header>
      <main className="container mx-auto p-8">
        <section className="text-center max-w-3xl mx-auto mb-16">
          <h1 className="text-5xl font-bold mb-4">
            Crypto AI Platform
          </h1>
          <p className="text-xl text-muted-foreground mb-8">
            Autonomous AI powered crypto intelligence platform
          </p>
          <Link href="/login">
            <Button size="lg">
              Get Started
              <ArrowRight className="ml-2 h-4 w-4" />
            </Button>
          </Link>
        </section>

        <section className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          {features.map((feature, i) => {
            const Icon = feature.icon;
            return (
              <div key={i} className="p-6 border rounded-lg hover:shadow-lg transition-shadow">
                <Icon className="h-12 w-12 mb-4 text-primary" />
                <h3 className="text-xl font-semibold mb-2">{feature.title}</h3>
                <p className="text-muted-foreground">{feature.description}</p>
              </div>
            );
          })}
        </section>
      </main>
    </div>
  );
}
