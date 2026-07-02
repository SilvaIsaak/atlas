# Contratos de Eventos
Todos os eventos da plataforma estão documentados aqui.

---

## 1. Regras de Nomenclatura
- Formato: `{Module}.{Action}.V{Major}.md`
- Exemplo: `MarketData.Ingested.V1.md`, `Feature.Created.V1.md`

## 2. Regras de Versionamento
- **Major**: Alterações breaking (remover campo obrigatório, alterar tipo de campo)
- **Minor**: Adicionar campos opcionais
- **Patch**: Correções de bugs no payload (não alteram contrato)

## 3. Regras de Backward Compatibility
- Sempre manter campos existentes
- Não alterar tipo de campos existentes
- Novos campos devem ser opcionais com valor padrão
- Consumers devem ignorar campos desconhecidos

## 4. Lista de Eventos Congelados
- `MarketData.Ingested.V1`
- Demais eventos serão documentados em breve (siga o padrão acima)
