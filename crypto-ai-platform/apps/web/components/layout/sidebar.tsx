'use client';

import { cn } from '@/lib/utils';
import {
  LayoutDashboard,
  TrendingUp,
  BarChart3,
  PieChart,
  Settings,
  User,
  Briefcase,
  AlertTriangle,
  Globe,
  BookOpen,
  Brain,
  Activity,
  DollarSign,
  LineChart,
  Zap,
  ShieldAlert,
  ListOrdered,
  FolderOpen,
} from 'lucide-react';
import Link from 'next/link';
import { usePathname } from 'next/navigation';

const navItems = [
  { href: '/dashboard', icon: LayoutDashboard, label: 'Dashboard' },
  
  { 
    label: 'Markets',
    icon: Globe,
    children: [
      { href: '/markets', icon: LineChart, label: 'Overview' },
      { href: '/markets/assets', icon: DollarSign, label: 'Assets' },
      { href: '/markets/orderbook', icon: ListOrdered, label: 'Order Book' },
    ]
  },

  {
    label: 'Trading',
    icon: TrendingUp,
    children: [
      { href: '/trading', icon: Zap, label: 'Terminal' },
      { href: '/trading/orders', icon: ListOrdered, label: 'Orders' },
      { href: '/trading/positions', icon: Briefcase, label: 'Positions' },
      { href: '/portfolio', icon: PieChart, label: 'Portfolio' },
    ]
  },

  {
    label: 'AI Engine',
    icon: Brain,
    children: [
      { href: '/ai/strategies', icon: Briefcase, label: 'Strategies' },
      { href: '/ai/agents', icon: User, label: 'Agents' },
      { href: '/ai/signals', icon: Activity, label: 'Signals' },
    ]
  },

  {
    label: 'Research',
    icon: BookOpen,
    children: [
      { href: '/research/experiments', icon: BarChart3, label: 'Experiments' },
      { href: '/research/dataset', icon: FolderOpen, label: 'Dataset' },
      { href: '/research/backtesting', icon: LineChart, label: 'Backtesting' },
    ]
  },

  {
    label: 'Risk',
    icon: ShieldAlert,
    children: [
      { href: '/risk/monitor', icon: Activity, label: 'Risk Monitor' },
      { href: '/risk/alerts', icon: AlertTriangle, label: 'Alerts' },
    ]
  },

  { href: '/settings', icon: Settings, label: 'Settings' },
];

export function Sidebar() {
  const pathname = usePathname();

  return (
    <div className="flex flex-col w-72 border-r bg-background">
      <div className="p-6 border-b">
        <h1 className="text-xl font-bold">Crypto AI</h1>
        <p className="text-sm text-muted-foreground">Platform</p>
      </div>
      <nav className="flex-1 p-4 space-y-1 overflow-y-auto">
        {navItems.map((section, idx) => {
          if ('children' in section) {
            return (
              <div key={idx} className="mb-4">
                <div className="flex items-center gap-2 px-4 py-2 text-sm font-medium text-muted-foreground">
                  <section.icon className="h-4 w-4" />
                  <span>{section.label}</span>
                </div>
                <div className="mt-1 space-y-1 ml-3">
                    {section.children?.map((item) => {
                    const isActive = pathname === item.href;
                    return (
                      <Link
                        key={item.href}
                        href={item.href}
                        className={cn(
                          'flex items-center gap-2 px-3 py-1.5 rounded-md text-sm transition-colors',
                          isActive
                            ? 'bg-primary text-primary-foreground'
                            : 'text-muted-foreground hover:bg-accent hover:text-accent-foreground'
                        )}
                      >
                        <item.icon className="h-4 w-4" />
                        <span>{item.label}</span>
                      </Link>
                    );
                  })}
                </div>
              </div>
            );
          }
          
          const isActive = pathname === section.href;
          return (
            <Link
              key={section.href}
              href={section.href}
              className={cn(
                'flex items-center gap-3 px-4 py-2 rounded-lg transition-colors',
                isActive
                  ? 'bg-primary text-primary-foreground'
                  : 'text-muted-foreground hover:bg-accent hover:text-accent-foreground'
              )}
            >
              <section.icon className="h-5 w-5" />
              <span>{section.label}</span>
            </Link>
          );
        })}
      </nav>
    </div>
  );
}
