# ADR-0001: Arquitetura DDD/Clean

## Contexto
- Domínio com regras específicas (eventos, chaveamento, ranking, RBAC).
- Requisitos de manutenibilidade e testes.

## Decisão
- Adotar DDD/Clean com 4 projetos: Domain, Application, Infrastructure, Api.
- Regras de referência estritas.
- Ports no Domain, casos de uso no Application.
- Infra com EF Core + MySQL, Redis, adapters de Auth.

## Consequências
- Separação clara de responsabilidades.
- Facilidade de teste e troca de infra.
- Mais arquivos/boilerplate inicial (aceitável para organização).
