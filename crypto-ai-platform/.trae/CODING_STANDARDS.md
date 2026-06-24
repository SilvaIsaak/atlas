# Crypto AI Platform - Coding Standards

## Índice
1. [Padrões de C#](#padrões-de-c)
2. [Padrões de TypeScript/React](#padrões-de-typescriptreact)
3. [Padrões de Nomenclatura Geral](#padrões-de-nomenclatura-geral)
4. [Formatação e Linting](#formatação-e-linting)
5. [Comentários](#comentários)

---

## Padrões de C#

### Estrutura Geral
- **Seguir o Framework Design Guidelines da Microsoft**
- **Manter arquivos com menos de 500 linhas** (exceto arquivos de configuração/gerados)
- **Métodos com menos de 40 linhas**
- **Uso de nullability checks habilitados globalmente** (Nullable = enable em todos os projetos)

### Nomenclatura em C#
| Item | Padrão | Exemplo |
|------|--------|---------|
| Classes, Records, Structs, Interfaces, Enums | PascalCase | `Strategy`, `ICreateStrategyHandler`, `OrderStatus` |
| Métodos | PascalCase | `CalculateSharpeRatio()`, `HandleAsync()` |
| Propriedades | PascalCase | `Id`, `CreatedAt`, `IsActive` |
| Constantes (campos `const`) | PascalCase | `MaxPositionSize` |
| Campos privados | _camelCase | `_logger`, `_dbContext` |
| Parâmetros de método | camelCase | `strategyId`, `initialCapital` |
| Variáveis locais | camelCase | `result`, `totalProfit` |
| Interfaces | Prefixo `I` | `IRepository<T>`, `IExchangeService` |

### Padrões de Uso
- **Preferir `record` para Value Objects e DTOs de transporte** (exceto quando mutabilidade for necessária)
- **Preferir `file-scoped namespaces`** (disponível em C# 10+)
- **Preferir `primary constructors`** para records/classes simples
- **Sempre usar `async/await` para operações I/O** (nunca usar `.Result` ou `.Wait()`, exceto em casos extremos como métodos `Main`)
- **Injetar dependências via construtor** (não usar `new` diretamente para serviços)
- **Sem `switch case` para lógica de negócio complexa** (usar Strategy Pattern ou Polymorphism)

### Exemplo de Arquivo C# (Application Layer)
```csharp
// Application/Features/Strategies/Commands/Create/CreateStrategyCommand.cs
using FluentValidation;
using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Application.Features.Strategies.Commands.Create;

public sealed record CreateStrategyCommand(
    string Name,
    string Description,
    decimal InitialCapital,
    List<Guid> IndicatorIds)
    : IRequest<Guid>;

public sealed class CreateStrategyCommandValidator : AbstractValidator<CreateStrategyCommand>
{
    public CreateStrategyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 100);

        RuleFor(x => x.InitialCapital)
            .GreaterThan(0);
    }
}
```

---

## Padrões de TypeScript/React

### Estrutura Geral
- **Seguir o guide do React + TypeScript da Vercel**
- **Preferir componentes funcinais com hooks**
- **Arquivos `.tsx` para componentes React**
- **Arquivos `.ts` para lógica utilitária, hooks customizados, types/interfaces**
- **Nomes de arquivos PascalCase para componentes React** (ex: `StrategyCard.tsx`)
- **Nomes de arquivos camelCase para hooks/utilitários** (ex: `useStrategy.ts`, `formatNumber.ts`)

### Nomenclatura em TypeScript/React
| Item | Padrão | Exemplo |
|------|--------|---------|
| Componentes React | PascalCase | `StrategyForm.tsx`, `Navbar.tsx` |
| Hooks customizados | `use` + PascalCase | `useStrategy`, `useMarketData` |
| Tipos, Interfaces | PascalCase | `StrategyDto`, `CreateStrategyRequest` |
| Constantes | UPPER_SNAKE_CASE | `API_BASE_URL`, `MAX_STRATEGY_NAME_LENGTH` |
| Variáveis e funções | camelCase | `handleSubmit`, `formData` |
| Props de componentes | Interface com sufixo `Props` | `StrategyCardProps` |

### Exemplo de Componente React
```tsx
// apps/web/components/features/strategies/StrategyCard.tsx
import { StrategyDto } from '@/types';
import { Button } from '@/components/ui/button';

interface StrategyCardProps {
  strategy: StrategyDto;
  onEdit: (id: string) => void;
  onDelete: (id: string) => void;
}

export function StrategyCard({
  strategy,
  onEdit,
  onDelete,
}: StrategyCardProps) {
  return (
    <div className="border rounded p-4">
      <h3 className="text-xl font-bold">{strategy.name}</h3>
      <p className="text-gray-600">{strategy.description}</p>
      <div className="flex gap-2 mt-4">
        <Button onClick={() => onEdit(strategy.id)}>Editar</Button>
        <Button variant="destructive" onClick={() => onDelete(strategy.id)}>
          Excluir
        </Button>
      </div>
    </div>
  );
}
```

---

## Padrões de Nomenclatura Geral

### Nomes de Arquivos e Pastas
- Pastas e arquivos de backend (C#): PascalCase (ex: `Strategies/Commands/Create/`)
- Pastas e arquivos de frontend (React):
  - Componentes: PascalCase (ex: `StrategyCard.tsx`)
  - Hooks/utilitários: camelCase (ex: `useStrategy.ts`)
  - Páginas do App Router: `page.tsx`, `layout.tsx`, `loading.tsx`, etc.

---

## Formatação e Linting

### Backend C#
- Usar `dotnet format` para formatação (baseado no `.editorconfig`)
- Usar analisadores do .NET (EnableNETAnalyzers = true, AnalysisLevel = latest)
- Tratar avisos como erros (TreatWarningsAsErrors = true) em builds de release

### Frontend TypeScript/React
- Usar Prettier para formatação
- Usar ESLint para linting (configurado no Next.js)
- Sem `any` e `unknown` excessivos (usar tipos específicos ou type guards)

---

## Comentários

- **Comentários apenas para explicar "por quê"**, não o "o quê" (o código deve explicar o "o quê")
- **Evitar comentários redundantes**
- **Documentar APIs públicas com XML Docs no C#**
- **Documentar componentes React/funções exportadas com JSDoc**

### Exemplo de XML Docs em C#
```csharp
/// <summary>
/// Calcula o Índice de Sharpe de uma estratégia.
/// </summary>
/// <param name="returns">Retornos da estratégia (taxas mensais/diárias)</param>
/// <param name="riskFreeRate">Taxa livre de risco anual</param>
/// <param name="periodsPerYear">Número de períodos por ano (ex: 12 para mensal, 252 para diário)</param>
/// <returns>Índice de Sharpe calculado</returns>
public decimal CalculateSharpeRatio(
    List<decimal> returns,
    decimal riskFreeRate,
    int periodsPerYear)
{
    // Implementação...
}
```
