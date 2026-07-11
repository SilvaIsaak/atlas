import { Loader2 } from 'lucide-react';

export function Loading({ fullScreen = false, size = 'md' }: { fullScreen?: boolean; size?: 'sm' | 'md' | 'lg' }) {
  const sizeClass = size === 'sm' ? 'h-4 w-4' : size === 'md' ? 'h-8 w-8' : 'h-12 w-12';

  if (fullScreen) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <Loader2 className={`animate-spin ${sizeClass}`} />
      </div>
    );
  }

  return <Loader2 className={`animate-spin ${sizeClass}`} />;
}
