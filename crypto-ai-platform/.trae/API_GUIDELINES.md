# Crypto AI Platform - API Guidelines

## Índice
1. [Padrões REST](#padrões-rest)
2. [Versionamento de API](#versionamento-de-api)
3. [OpenAPI/Swagger](#openapiswagger)
4. [Códigos de Status HTTP](#códigos-de-status-http)
5. [Paginação, Ordenação, Filtro](#paginação-ordenação-filtro)
6. [Validação e Tratamento de Erros](#validação-e-tratamento-de-erros)
7. [Autenticação e Autorização](#autenticação-e-autorização)

---

## Padrões REST

### Nomenclatura de Recursos
- **Recursos em plural e kebab-case** (ex: `/strategies`, `/market-data`)
- **Sub-recursos aninhados** (ex: `/strategies/{id}/indicators`, `/users/{id}/orders`)
- **Evitar verbos na URL** (usar métodos HTTP para indicar ação)

### Métodos HTTP
| Método | Ação | Idempotente |
|--------|------|-------------|
| GET | Buscar recurso(s) | Sim |
| POST | Criar recurso | Não |
| PUT | Atualizar recurso completo | Sim |
| PATCH | Atualizar recurso parcialmente | Não |
| DELETE | Excluir recurso | Sim |

---

## Versionamento de API
- Usar **versionamento via URL** (ex: `/api/v1/strategies`)
- Também suportar via header `X-Api-Version` (para casos especiais)
- Versões em formato `v{Major}` (sem Minor/Patch para simplicidade inicial)
- Quando lançar uma nova versão, manter a versão antiva por pelo menos 6 meses

---

## OpenAPI/Swagger
- Documentar **todos os endpoints públicos** com Swagger/OpenAPI
- Usar `Swashbuckle.AspNetCore` para gerar OpenAPI automaticamente no backend
- Incluir:
  - Descrição da API
  - Parâmetros de entrada (tipos, obrigatoriedade, exemplos)
  - Respostas (códigos de status, schemas)
  - Exemplos de request/response

---

## Códigos de Status HTTP
Aplicar consistentemente os seguintes códigos:
| Código | Significado |
|--------|-------------|
| 200 OK | Sucesso (GET/PUT/PATCH) |
| 201 Created | Recurso criado com sucesso (POST) — retornar header `Location` com a URL do novo recurso |
| 204 No Content | Sucesso sem conteúdo de retorno (DELETE) |
| 400 Bad Request | Erro de validação do cliente (dados inválidos) |
| 401 Unauthorized | Falha na autenticação (token ausente/inválido) |
| 403 Forbidden | Autenticado, mas sem permissão para acessar o recurso |
| 404 Not Found | Recurso não encontrado |
| 409 Conflict | Conflito (ex: recurso já existe) |
| 422 Unprocessable Entity | Entidade não processável (validação semântica) |
| 429 Too Many Requests | Rate limiting atingido |
| 500 Internal Server Error | Erro interno do servidor |
| 503 Service Unavailable | Serviço temporariamente indisponível |

---

## Paginação, Ordenação, Filtro

### Paginação
- **Parâmetros padrão**: `pageNumber` (padrão 1) e `pageSize` (padrão 20, máximo 100)
- **Retornar metadata** no response header `X-Pagination` ou no body (para conveniência):
  ```json
  {
    "data": [...],
    "pagination": {
      "pageNumber": 1,
      "pageSize": 20,
      "totalPages": 10,
      "totalCount": 198
    }
  }
  ```

### Ordenação
- Parâmetro `sortBy` (ex: `sortBy=createdAt`, `sortBy=name,asc`)
- Direção padrão: `asc`

### Filtro
- Parâmetros de filtro por propriedades do recurso (ex: `/strategies?status=active&minCapital=1000`)

---

## Validação e Tratamento de Erros
- Usar **FluentValidation** para validar Commands/Queries na Application Layer
- Padronizar o formato de resposta de erro:
  ```json
  {
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "Bad Request",
    "status": 400,
    "traceId": "00-123456789abcdef...",
    "errors": {
      "name": [
        "O nome deve ter entre 3 e 100 caracteres"
      ],
      "initialCapital": [
        "O capital inicial deve ser maior que zero"
      ]
    }
  }
  ```
- **Não retornar detalhes de exceção sensíveis em produção** (ex: stack trace)
- Logar erros completos internamente (com Serilog)

---

## Autenticação e Autorização
- Usar **JWT com chaves assimétricas (RS256)**
- Token no header `Authorization: Bearer <token>`
- Autorização via **Roles e Policies** (usar ASP.NET Core Authorization)
- 2FA/OTP obrigatório para usuários (tarefa posterior)
