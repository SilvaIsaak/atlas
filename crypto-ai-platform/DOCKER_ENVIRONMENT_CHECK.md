# Docker Environment Check

## Resultado do docker-compose up -d
- Imagens baixadas com sucesso
- Network `phase0_default` criada
- Volume `phase0_postgres_data` criado
- Containers `phase0-rabbitmq-1` e `phase0-postgres-1` criados
- Erro: `Bind for 0.0.0.0:6379 failed: port is already allocated` (porta Redis ocupada)

## Ação Necessária
Liberar a porta 6379 ou alterar a porta no docker-compose.yml

## Containers Esperados
- [x] PostgreSQL/TimescaleDB (porta 5432)
- [ ] Redis (porta 6379)
- [x] RabbitMQ (portas 5672 e 15672)
