# UCondo – Accounts API

Template **Ardalis Clean Architecture** • **SQLite** • **FastEndpoints** • **Entity Framework Core** • **MediatR**

> Backend para **Plano de Contas** (códigos hierárquicos como `1.2.9`), implementado no **template do Ardalis Clean Arch
** com **FastEndpoints** para a API, **EF Core** para persistência em **SQLite** e **MediatR** para orquestrar use
cases.

---

## 📦 Tecnologias & Decisões

- **Clean Architecture (Ardalis)**: separação clara de responsabilidades.
- **SQLite (arquivo no repo)**: simples, multiplataforma, sem Docker.
- **EF Core 9**: writes e consultas padrão.
- **MediatR**: comandos/queries desacoplados.
- **FastEndpoints**: endpoints enxutos e tipados, com OpenAPI/Swagger.
- **Value Object `AccountCode`**: encapsula o código hierárquico (parse/print/compare).

> Observação: quando necessário, as consultas que exigem “prefixo + profundidade” são escritas de forma **compatível com
EF/SQLite** (usando operações de string) para evitar problemas de tradução LINQ.

---

## 🧱 Estrutura (padrão Ardalis)

```
  Core/                # Domínio (Entities, ValueObjects, Enums, Exceptions, Specifications puras)
  UseCases/            # Application (MediatR: Commands, Queries, DTOs, Handlers)
  Infrastructure/      # EF Core: DbContext, Mappings; (optionals: read helpers)
  Web/                 # API FastEndpoints, Program.cs/DI, Swagger
```

---

## 🧠 Regras de Negócio (resumo)

- **Código único** no plano (índice/validação).
- **Mesmo tipo do pai** (Asset/Liability/Equity/Revenue/Expense).
- **Conta que aceita lançamentos** não pode ter **filhos**.
- **Sugestão de código** = sempre **“maior + 1”**, com **teto 999** por segmento.
- Caso `1.2.999` exista, a sugestão considera o **novo pai apropriado** conforme regra de negócio.

---

## ⚙️ Configuração & Execução

### 1) Pré-requisitos

- **.NET SDK 9.x**

### 2) Restaurar & Compilar

```bash
dotnet restore
dotnet build
```

### 3) Migrations

```bash
dotnet tool update --global dotnet-ef --version 9.0.8

dotnet ef migrations add --project Ucondo.Infrastructure\Ucondo.Infrastructure.csproj --startup-project Ucondo.Web\Ucondo.Web.csproj --context Ucondo.Infrastructure.Data.UcondoDbContext --configuration Debug teste --output-dir Data\Migrations
dotnet ef database update --project Ucondo.Infrastructure\Ucondo.Infrastructure.csproj --startup-project Ucondo.Web\Ucondo.Web.csproj --context Ucondo.Infrastructure.Data.UcondoDbContext --configuration Debug 20250813110558_ChangeFieldNameAccountTable
```