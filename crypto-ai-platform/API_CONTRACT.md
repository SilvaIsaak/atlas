# Contrato de API (API Contract) v1.0

## Base URL
`{scheme}://{host}:{port}/api/v1`

## Controle de Versão
A versão da API é definida no caminho da URL (ex: `/api/v1/...`). Qualquer alteração que quebre a compatibilidade deve incrementar a versão major.

## Autenticação
*A ser implementada: JWT Bearer Token*

## Autorização
*A ser implementada: Roles e Permissões*

## Endpoints

### 1. Autenticação e Autorização (`/auth`)

#### POST /auth/register
Registra um novo usuário.
- Request: `RegisterUserCommand`
- Response: 201 Created (`RegisterUserResponse`) | 400 Bad Request

#### POST /auth/login
Realiza login de usuário.
- Request: `LoginUserCommand`
- Response: 200 OK (`LoginUserResponse`) | 401 Unauthorized

#### POST /auth/roles
Cria um novo papel (role).
- Request: `CreateRoleCommand`
- Response: 201 Created (`CreateRoleResponse`) | 400 Bad Request
- Autorização: Admin

#### GET /auth/roles
Obtém todos os papéis.
- Response: 200 OK (List<GetRoleByIdResponse>)
- Autorização: Admin

#### GET /auth/roles/{roleId}
Obtém papel por ID.
- Parâmetros: `roleId` (Guid)
- Response: 200 OK (`GetRoleByIdResponse`) | 404 Not Found
- Autorização: Admin

#### PUT /auth/roles/{roleId}
Atualiza papel.
- Parâmetros: `roleId` (Guid)
- Request: `UpdateRoleCommand`
- Response: 200 OK (`GetRoleByIdResponse`) | 400 Bad Request
- Autorização: Admin

#### DELETE /auth/roles/{roleId}
Remove papel.
- Parâmetros: `roleId` (Guid)
- Response: 200 OK | 404 Not Found
- Autorização: Admin

#### POST /auth/roles/{roleId}/permissions
Atribui permissão a papel.
- Parâmetros: `roleId` (Guid)
- Request: `AssignPermissionToRoleCommand`
- Response: 200 OK (bool) | 404 Not Found
- Autorização: Admin

#### DELETE /auth/roles/{roleId}/permissions/{permission}
Remove permissão de papel.
- Parâmetros: `roleId` (Guid), `permission` (Permission)
- Response: 200 OK (bool) | 404 Not Found
- Autorização: Admin

#### GET /auth/permissions
Obtém todas as permissões.
- Response: 200 OK (List<PermissionDto>)
- Autorização: Admin

#### POST /auth/users/{userId}/roles
Atribui papel a usuário.
- Parâmetros: `userId` (Guid)
- Request: `AssignRoleCommand`
- Response: 200 OK (`AssignRoleResponse`) | 400 Bad Request
- Autorização: Admin

#### GET /auth/users
Obtém todos os usuários.
- Response: 200 OK (List<GetUserByIdResponse>)
- Autorização: Admin

#### GET /auth/users/{userId}
Obtém usuário por ID.
- Parâmetros: `userId` (Guid)
- Response: 200 OK (`GetUserByIdResponse`) | 404 Not Found
- Autorização: Autenticado

#### PUT /auth/users/{userId}
Atualiza usuário.
- Parâmetros: `userId` (Guid)
- Request: `UpdateUserCommand`
- Response: 200 OK (`UpdateUserResponse`) | 400 Bad Request
- Autorização: Autenticado

#### DELETE /auth/users/{userId}
Remove usuário.
- Parâmetros: `userId` (Guid)
- Response: 200 OK | 404 Not Found
- Autorização: Admin

---

### 2. Indicadores Técnicos (`/indicators`)

#### POST /indicators/sma
Calcula Média Móvel Simples (SMA).
- Request: `CalculateSmaQuery`
- Response: 200 OK (`SmaResult`)
- Autorização: Autenticado

#### POST /indicators/ema
Calcula Média Móvel Exponencial (EMA).
- Request: `CalculateEmaQuery`
- Response: 200 OK (`EmaResult`)
- Autorização: Autenticado

#### POST /indicators/rsi
Calcula Índice de Força Relativa (RSI).
- Request: `CalculateRsiQuery`
- Response: 200 OK (`RsiResult`)
- Autorização: Autenticado

#### POST /indicators/macd
Calcula MACD.
- Request: `CalculateMacdQuery`
- Response: 200 OK (`MacdResult`)
- Autorização: Autenticado

#### POST /indicators/bollinger-bands
Calcula Bandas de Bollinger.
- Request: `CalculateBollingerBandsQuery`
- Response: 200 OK (`BollingerBandsResult`)
- Autorização: Autenticado

---

### 3. Dados de Mercado (`/market`)

#### GET /market/ticker
Obtém ticker (preço atual) de um par.
- Parâmetros: `exchangeCode` (string), `symbol` (string)
- Response: 200 OK (`ExchangeTicker`)
- Autorização: Autenticado

#### GET /market/orderbook
Obtém livro de ordens de um par.
- Parâmetros: `exchangeCode` (string), `symbol` (string), `limit` (int, opcional, padrão: 20)
- Response: 200 OK (`ExchangeOrderBook`)
- Autorização: Autenticado

#### GET /market/klines
Obtém candles históricos de um par.
- Parâmetros: `exchangeCode` (string), `symbol` (string), `interval` (string), `startTime` (DateTime, opcional), `endTime` (DateTime, opcional)
- Response: 200 OK (List<ExchangeKline>)
- Autorização: Autenticado

---

## Observações
- Todos os endpoints que aceitam body usam `application/json`.
- Todos os endpoints que retornam dados usam `application/json`.
- Erros são retornados como Problem Details (padrão ASP.NET Core).
