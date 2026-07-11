'use client';

import { ProtectedRoute } from '@/components/protected-route';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { useDataStore } from '@/lib/stores/useDataStore';
import { useEffect } from 'react';
import { Brain, TrendingUp, Shield, Briefcase, DollarSign } from 'lucide-react';

const agentIcons = {
  supervisor: Brain,
  market: TrendingUp,
  risk: Shield,
  strategy: Briefcase,
  portfolio: DollarSign,
};

export default function AiEnginePage() {
  const { agents, init } = useDataStore();

  useEffect(() => {
    init();
  }, [init]);

  return (
    <ProtectedRoute>
      <div className="space-y-6">
        <div>
          <h1 className="text-3xl font-bold">AI Engine</h1>
          <p className="text-muted-foreground">Intelligent agents monitoring the market 24/7</p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {agents.map((agent) => {
            const Icon = agentIcons[agent.type as keyof typeof agentIcons] || Brain;
            const isOnline = agent.status === 'running';

            return (
              <Card key={agent.id} className="hover:shadow-lg transition-shadow">
                <CardHeader>
                  <div className="flex items-center justify-between">
                    <CardTitle className="flex items-center gap-2">
                      <Icon className="h-5 w-5 text-primary" />
                      {agent.name}
                    </CardTitle>
                    <div className="flex items-center gap-2">
                      <div className={`h-3 w-3 rounded-full ${
                        isOnline ? 'bg-green-500' : agent.status === 'stopped' ? 'bg-gray-400' : 'bg-red-500'
                      }`} />
                      <span className="text-xs font-medium capitalize">{agent.status}</span>
                    </div>
                  </div>
                </CardHeader>
                <CardContent className="space-y-4">
                  {agent.decisions && agent.decisions.length > 0 && (
                    <div>
                      <p className="text-sm text-muted-foreground mb-1">Last Decision</p>
                      <p className="font-semibold">{agent.decisions[0]}</p>
                    </div>
                  )}

                  {agent.confidence !== undefined && (
                    <div>
                      <div className="flex justify-between items-center mb-2">
                        <span className="text-sm text-muted-foreground">Confidence</span>
                        <span className="font-bold">{agent.confidence}%</span>
                      </div>
                      <div className="w-full bg-muted rounded-full h-2">
                        <div
                          className="bg-primary h-2 rounded-full"
                          style={{ width: `${agent.confidence}%` }}
                        />
                      </div>
                    </div>
                  )}

                  {agent.reason && (
                    <div>
                      <span className="text-sm text-muted-foreground block mb-1">Reasoning</span>
                      <p className="text-sm">{agent.reason}</p>
                    </div>
                  )}
                </CardContent>
              </Card>
            );
          })}
        </div>
      </div>
    </ProtectedRoute>
  );
}
