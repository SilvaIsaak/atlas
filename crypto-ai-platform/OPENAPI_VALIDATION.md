# Validação do OpenAPI

## Status
✅ **Configuração Básica Concluída**

## Configuração Atual
- Swashbuckle.AspNetCore instalado no projeto Presentation.
- Geração de arquivo XML de documentação ativada.
- Comentários XML incluídos na documentação Swagger.
- Versionamento de API via URL (Asp.Versioning.Mvc e Asp.Versioning.Mvc.ApiExplorer).

## Como Gerar o Arquivo OpenAPI/Swagger
1. Compile a solução: `dotnet build`.
2. Execute a API: `dotnet run --project apps/api-core/CryptoAIPlatform.Api.csproj`.
3. Acesse `http://localhost:{port}/swagger/v1/swagger.json` para baixar o arquivo OpenAPI.
4. Acesse `http://localhost:{port}/swagger` para visualizar a interface Swagger UI.

## Próximos Passos para Validação Completa
- Adicionar exemplo de requisições/respostas nos endpoints.
- Configurar autenticação JWT no Swagger.
- Validar o contrato OpenAPI com ferramentas como Spectral.
