'use client';
import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { useQuery } from '@tanstack/react-query';
import { aiService } from '@/lib/services';
export default function AgentsPage() {
  const { data: agents } = useQuery({ queryKey: ['agents'], queryFn: aiService.getAgents });
  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <h1 className="text-3xl font-bold">AI Agents</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {agents?.map(agent => (
            <Card key={agent.id}>
              <CardHeader>
                <div className="flex justify-between items-center">
                  <CardTitle>{agent.name}</CardTitle>
                  <div className="flex items-center gap-2">
                    <div className={`h-2 w-2 rounded-full ${
                      agent.status === 'running' ? 'bg-green-500' : 
                      agent.status === 'stopped' ? 'bg-gray-400' : 'bg-red-500'
                    }`} />
                    <span className="text-sm capitalize text-muted-foreground">{agent.status}</span>
                  </div>
                </div>
              </CardHeader>
              <CardContent className="space-y-4">
                <div>
                  <p className="text-sm font-medium mb-1">Type</p>
                  <p className="text-sm text-muted-foreground">{agent.type}</p>
                </div>
                <div>
                  <p className="text-sm font-medium mb-1">Last Execution</p>
                  <p className="text-sm text-muted-foreground">{agent.lastExecution.toLocaleString()}</p>
                </div>
                <div>
                  <p className="text-sm font-medium mb-1">Recent Decisions</p>
                  <div className="flex flex-wrap gap-1">
                    {agent.decisions.map((d, i) => (
                      <span key={i} className="text-xs bg-accent px-2 py-1 rounded">{d}</span>
                    ))}
                  </div>
                </div>
                <div className="flex gap-2">
                  <Button className="flex-1">Start</Button>
                  <Button variant="secondary" className="flex-1">Stop</Button>
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      </div>
    </ProtectedRoute>
  );
}
