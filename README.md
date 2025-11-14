# blue-desafio
Desafio para o processo seletivo da empresa Blue Technology

Autor: Victor Padilha
victorportopadilha@gmail.com

# Documentação

## Visão Geral

Aplicação full-stack para gerenciamento de contatos com Clean Architecture e CQRS.

## Stack Tecnológico

- **Backend:** ASP.NET Core 8
- **Frontend:** Vue 3 + TypeScript + Vite
- **Database:** PostgreSQL 15
- **Documentação APIs:** Swagger

---

## Arquitetura do Backend

```
AgendaApp.API/              # Controllers
AgendaApp.Application/      # CQRS + Handlers + Validators
AgendaApp.Infrastructure/   # Repositórios + EF Core
AgendaApp.Domain/           # Entities + Interfaces
```

Padrões: **CQRS**, **MediatR**, **FluentValidation**, **AutoMapper**, **Repository Pattern**

---

## Docker

```bash
docker-compose build
docker-compose up -d
docker-compose ps
```

**Acessar:**
- Frontend: http://localhost:3000

**Parar:**
```bash
docker-compose down
```

---

## Testes

**Backend (unitários):**
```bash
cd backend
dotnet test 
```

**Frontend:**
```bash
cd frontend
npm test
```

---

## API

| Método | Endpoint |
|--------|----------|
| GET | `/api/contacts` |
| POST | `/api/contacts` |
| PUT | `/api/contacts/{id}` |
| DELETE | `/api/contacts/{id}` |