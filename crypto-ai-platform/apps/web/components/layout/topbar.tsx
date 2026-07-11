'use client';

import { useAuthStore } from '@/lib/stores/useAuthStore';
import { Button } from '@/components/ui/button';
import { Bell, Search, User, Moon, Sun } from 'lucide-react';
import { useTheme } from 'next-themes';

export function Topbar() {
  const { user, logout } = useAuthStore();
  const { theme, setTheme } = useTheme();

  return (
    <header className="h-16 border-b flex items-center justify-between px-6">
      <div className="flex items-center gap-4">
        <div className="relative">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground" />
          <input
            type="text"
            placeholder="Pesquisar..."
            className="pl-10 pr-4 py-2 rounded-lg border bg-background w-80"
          />
        </div>
      </div>
      <div className="flex items-center gap-4">
        <Button
          variant="ghost"
          size="icon"
          onClick={() => setTheme(theme === 'dark' ? 'light' : 'dark')}
        >
          {theme === 'dark' ? <Sun className="h-5 w-5" /> : <Moon className="h-5 w-5" />}
        </Button>
        <Button variant="ghost" size="icon">
          <Bell className="h-5 w-5" />
        </Button>
        <div className="flex items-center gap-3">
          <div className="text-right">
            <p className="text-sm font-medium">{user?.firstName} {user?.lastName}</p>
            <p className="text-xs text-muted-foreground">{user?.email}</p>
          </div>
          <div className="h-10 w-10 rounded-full bg-primary flex items-center justify-center text-primary-foreground">
            <User className="h-5 w-5" />
          </div>
          <Button variant="ghost" onClick={logout}>
            Sair
          </Button>
        </div>
      </div>
    </header>
  );
}
