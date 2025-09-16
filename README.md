# Radar das Batalhas - Backend

Stack: .NET 9 Web API, EF Core MySQL, Redis, Serilog, AutoMapper, Keycloak, xUnit.

## Estrutura
- src/Domain, src/Application, src/Infrastructure, src/Api
- tests/Unit, tests/Integration

## Setup local
1. Copie `.env.example` para `.env` e ajuste.
2. Docker build e subir stack:
   - docker build -t radar-api:dev -f ./Dockerfile .
   - docker compose up -d
3. Logs: docker compose logs -f api
4. Parar: docker compose down

## Migrations
- Em dev, aplique via: docker compose exec api dotnet ef database update (placeholder, migrations serão adicionadas).

## Auth (Keycloak)
- Suba o Keycloak (docker-compose)
- Crie realm `radar`, client `radar-api` (confidential), roles/papéis e permissões (ver RBAC)
- Defina `KEYCLOAK_CLIENT_SECRET` no `.env`

## RBAC (seed inicial)
- Papéis: ADMINISTRADOR, ORGANIZADOR, MC, VISUALIZADOR
- Permissões: ver seção do prompt (eventos.*, noticias.*, ranking.ler, etc.)

## API Base
- BasePath: /api/v1
- Envelope: { data, correlationId }
- Errors: { status, code, message, correlationId }

## Health
- GET /health

## OpenAPI
- veja openapi.yaml na raiz

## Testes
- Unit: AAA, Moq
- Integration: usa autenticação fake no ambiente `Test`

## Observabilidade
- Serilog (console JSON)
- CorrelationId middleware

## Pastas por feature
- Application/Eventos/UseCases, Api/Controllers/Admin etc.

## Próximos passos
- Completar use cases e repositórios
- Migrations + seed RBAC e Status
