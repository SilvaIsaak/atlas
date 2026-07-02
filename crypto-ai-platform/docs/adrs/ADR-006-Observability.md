# ADR-006: Estratégia de Observabilidade
**Status**: Aprovado  
**Data**: 2026-06-25  
**Decisores**: Lead Architect, CTO, DevOps Lead  
**Contexto**: Definir como monitorar, logar e traçar a plataforma

---

## 1. Decisão
- **Logs**:
  - Estruturados (JSON)
  - Serilog como biblioteca
  - Destinos: Console, Arquivo (rotativo), OpenTelemetry → Loki
- **Métricas**:
  - OpenTelemetry
  - Destino: Prometheus
- **Tracing**:
  - OpenTelemetry
  - Destino: Jaeger/Tempo
- **Dashboards**: Grafana
- **Alertas**: Prometheus AlertManager (Slack/Email/PagerDuty)

## 2. Justificativa
- OpenTelemetry é padrão da indústria, evita lock-in
- Grafana + Prometheus + Loki + Jaeger é uma stack comprovada
- Serilog é fácil de usar e integrar

## 3. Alternativas Consideradas
- ELK Stack: Mais complexo, OpenTelemetry + Grafana Stack foi escolhido por simplicidade e alinhamento com a equipe
- Datadog/New Relic: Caro em escala, ferramentas open-source preferidas para Phase 0

## 4. Consequências
- **Positivas**: Observabilidade completa, padrão da indústria, low cost
- **Negativas**: Múltiplas ferramentas para gerenciar
