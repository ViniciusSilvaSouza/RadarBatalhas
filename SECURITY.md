# SECURITY

- Auth: JWT (OIDC Keycloak). Em testes, handler fake.
- RBAC: em banco, ver seed planejado (papéis e permissões). Middleware exige permissões com IServicoAutorizacao.
- Logging: Serilog JSON no console. CorrelationId middleware para rastreamento.
- Dados sensíveis: usar .env (não versionar em produção). Nunca logar segredos.
- TLS: configurar nas camadas de deploy (reverse proxy). Dev usa HTTP.
- Dependências: GitHub Actions restaura e compila a cada PR; adicionar SAST futuramente.
