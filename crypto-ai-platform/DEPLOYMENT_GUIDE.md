# Deployment Guide

## Variáveis de Ambiente

### Frontend
- NEXT_PUBLIC_API_URL: URL da API

### Backend
- ConnectionStrings__DefaultConnection: String de conexão do banco
- JWT__Secret: Chave secreta para JWT
- JWT__Issuer: Issuer do JWT
- JWT__Audience: Audience do JWT

## Docker
- Use docker-compose.yml na raiz do crypto-ai-platform

## Banco de Dados
- Aplicar migrations (dotnet ef database update)

## Deploy Frontend
- npm run build
- Deploy para Vercel/Netlify/static hosting

## Deploy Backend
- dotnet publish
- Deploy para Azure/AWS/container

