# Crypto AI Platform - Design System
## Índice
1. [Princípios](#princípios)
2. [Cores & Design Tokens](#cores--design-tokens)
3. [Tipografia](#tipografia)
4. [Espaçamentos](#espaçamentos)
5. [Bordas & Radius](#bordas--radius)
6. [Ícones](#ícones)
7. [Componentes shadcn/ui](#componentes-shadcnui)

---

## Princípios
- **Clareza**: Interface intuitiva e fácil de usar
- **Consistência**: Mesmos padrões em toda a plataforma
- **Acessibilidade**: Seguir WCAG 2.1 AA (contraste, navegação por teclado, etc.)
- **Performance**: Carregar rápido, sem delays
- **Responsividade**: Funcionar em mobile, tablet e desktop

---

## Cores & Design Tokens
### Dark Mode (Padrão)
| Token | HSL | Hex (Aproximado) |
|-------|-----|------------------|
| `--background` | 222.2 84% 4.9% | #020617 |
| `--foreground` | 210 40% 98% | #f9fafb |
| `--primary` | 217.2 91.2% 59.8% | #2563eb |
| `--primary-foreground` | 222.2 47.4% 11.2% | #020617 |
| `--secondary` | 217.2 32.6% 17.5% | #1e293b |
| `--secondary-foreground` | 210 40% 98% | #f9fafb |
| `--destructive` | 0 62.8% 30.6% | #991b1b |
| `--destructive-foreground` | 210 40% 98% | #f9fafb |
| `--muted` | 217.2 32.6% 17.5% | #1e293b |
| `--muted-foreground` | 215 20.2% 65.1% | #94a3b8 |
| `--accent` | 217.2 32.6% 17.5% | #1e293b |
| `--accent-foreground` | 210 40% 98% | #f9fafb |
| `--card` | 222.2 84% 4.9% | #020617 |
| `--card-foreground` | 210 40% 98% | #f9fafb |
| `--popover` | 222.2 84% 4.9% | #020617 |
| `--popover-foreground` | 210 40% 98% | #f9fafb |
| `--border` | 217.2 32.6% 17.5% | #1e293b |
| `--input` | 217.2 32.6% 17.5% | #1e293b |
| `--ring` | 224.3 76.3% 48% | #1d4ed8 |
| `--radius` | 0.5rem | 0.5rem |

### Light Mode
| Token | HSL | Hex (Aproximado) |
|-------|-----|------------------|
| `--background` | 0 0% 100% | #ffffff |
| `--foreground` | 222.2 84% 4.9% | #020617 |
| `--primary` | 221.2 83.2% 53.3% | #1d4ed8 |
| `--primary-foreground` | 210 40% 98% | #f9fafb |
| `--secondary` | 210 40% 96.1% | #f3f4f6 |
| `--secondary-foreground` | 222.2 47.4% 11.2% | #020617 |
| `--destructive` | 0 84.2% 60.2% | #dc2626 |
| `--destructive-foreground` | 210 40% 98% | #f9fafb |
| `--muted` | 210 40% 96.1% | #f3f4f6 |
| `--muted-foreground` | 215.4 16.3% 46.9% | #64748b |
| `--accent` | 210 40% 96.1% | #f3f4f6 |
| `--accent-foreground` | 222.2 47.4% 11.2% | #020617 |
| `--card` | 0 0% 100% | #ffffff |
| `--card-foreground` | 222.2 84% 4.9% | #020617 |
| `--popover` | 0 0% 100% | #ffffff |
| `--popover-foreground` | 222.2 84% 4.9% | #020617 |
| `--border` | 214.3 31.8% 91.4% | #e2e8f0 |
| `--input` | 214.3 31.8% 91.4% | #e2e8f0 |
| `--ring` | 221.2 83.2% 53.3% | #1d4ed8 |
| `--radius` | 0.5rem | 0.5rem |

---

## Tipografia
- **Fonte Principal**: Inter (sans-serif)
- **Tamanhos (em rem)**:
  - H1: 2.5rem (40px)
  - H2: 2rem (32px)
  - H3: 1.75rem (28px)
  - H4: 1.5rem (24px)
  - Body: 1rem (16px)
  - Pequeno (Muted): 0.875rem (14px)
- **Pesos**:
  - Regular: 400
  - Medium: 500
  - Semibold: 600
  - Bold: 700

---

## Espaçamentos
Escala baseada em 4px:
| Tamanho (px) | Classe Tailwind |
|--------------|-----------------|
| 4 | `space-y-1` |
| 8 | `space-y-2` |
| 12 | `space-y-3` |
| 16 | `space-y-4` |
| 24 | `space-y-6` |
| 32 | `space-y-8` |
| 40 | `space-y-10` |
| 48 | `space-y-12` |
| 64 | `space-y-16` |

---

## Bordas & Radius
- **Border Radius**: 0.5rem (`rounded-md`), 0.75rem (`rounded-lg`)
- **Box Shadow**: `shadow-sm`, `shadow-md`, `shadow-lg`
- **Hover/Active States**:
  - Focus: `ring-2 ring-offset-2 ring-ring`
  - Hover: Mudança de cor em 0.2s
  - Disabled: `opacity-50 pointer-events-none`

---

## Ícones
- **Biblioteca**: Lucide React
- **Tamanhos**: 16px (w-4 h-4), 20px (w-5 h-5), 24px (w-6 h-6)
- **Padrão**: Sempre usar `Lucide`; NÃO misturar bibliotecas

---

## Componentes shadcn/ui
Componentes já implementados (em `@/components/ui/`):
- Button
- Card (CardHeader, CardTitle, CardDescription, CardContent, CardFooter)
- Input
- Label
- Outros componentes a serem adicionados:
  - DataTable
  - Dialog
  - Tabs
  - Select
  - Badge
  - Avatar
  - Tooltip
  - Etc.

