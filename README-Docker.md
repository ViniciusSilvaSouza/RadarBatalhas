# Docker - Radar das Batalhas

## Comandos (PowerShell)
- Build API:
  - docker build -t radar-api:dev -f ./Dockerfile .
- Subir stack:
  - docker compose up -d
- Logs API:
  - docker compose logs -f api
- Parar/Remover:
  - docker compose down
- Aplicar migrations (se não automatizado):
  - docker compose exec api dotnet ef database update

## Serviços e portas
- API: 5000 -> 8080
- Keycloak: 8080
- MySQL: 3306
- Redis: 6379
- MinIO: 9000/9001
- Adminer: 8081

## Configuração Keycloak
1. Acesse http://localhost:8080
2. Login admin/admin
3. Crie Realm: `radar`
4. Crie Client: `radar-api` (confidential, standard flow ON)
5. Configure Valid Redirect URIs: http://localhost:5000/*
6. Crie Roles (realm): ADMINISTRADOR, ORGANIZADOR, MC, VISUALIZADOR
7. Crie usuários de teste e atribua roles
8. Copie o `Client Secret` para KEYCLOAK_CLIENT_SECRET no `.env`

## Variáveis de ambiente
- DB_HOST, DB_PORT, DB_NAME, DB_USER, DB_PASSWORD
- REDIS_HOST
- KEYCLOAK_AUTH_URL, KEYCLOAK_REALM, KEYCLOAK_CLIENT_ID, KEYCLOAK_CLIENT_SECRET
- MINIO_ENDPOINT, MINIO_ACCESS_KEY, MINIO_SECRET_KEY
- ASPNETCORE_ENVIRONMENT, LOG_LEVEL

## Notas
- Tracing/métricas: placeholders (não implementado)
- Migrations e seed RBAC virão em próxima etapa
