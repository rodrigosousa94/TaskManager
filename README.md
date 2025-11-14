# 📝 TaskManager API

Uma API REST desenvolvida em **ASP.NET Core 8**, utilizando **Entity Framework Core**, **MySQL** e arquitetura em camadas.  
O objetivo do projeto é permitir o gerenciamento de tarefas (CRUD) de forma organizada, seguindo boas práticas, princípios SOLID e Clean Code.

---

## 🚀 Tecnologias Utilizadas

- **ASP.NET Core 8 – Web API**
- **Entity Framework Core 8**
- **MySQL Server**
- **Swagger / Swashbuckle**
- **Clean Architecture / Layered Architecture**

---

## 📁 Arquitetura do Projeto

TaskManager/
│── TaskManager.API/ → Camada de apresentação (Controllers e configuração)
│── TaskManager.Application/ → Serviços, DTOs, regras de aplicação
│── TaskManager.Domain/ → Entidades, enums e contratos
│── TaskManager.Infrastructure/ → Banco de dados, contexto, repositórios


### 🔍 Descrição das camadas

| Camada | Responsabilidade |
|--------|------------------|
| **Domain** | Entidades, enums, interfaces e regras essenciais |
| **Application** | DTOs, serviços e lógica de aplicação |
| **Infrastructure** | EF Core, repositórios, DBContext e conexão com banco |
| **API** | Controllers, endpoints HTTP e configuração geral |


## 📄 Estrutura da Entidade Task

| Campo | Tipo | Regras |
|-------|------|---------|
| Id | int | Auto incremento |
| Title | string | Obrigatório, máx. 100 caracteres |
| Description | string | Opcional |
| CreatedAt | datetime | Definido automaticamente |
| CompletedAt | datetime? | Definido automaticamente ao concluir |
| Status | enum | 0 = Pending, 1 = InProgress, 2 = Completed |

---

## 🔧 Configurando o Ambiente

### 1️⃣ Clonar o repositório

git clone https://github.com/rodrigosousa94/TaskManager.git
cd TaskManager
🔑 Variáveis de Ambiente (MySQL)
O projeto utiliza variáveis ambiente na connection string.

setx DB_PASSWORD - senha conexao com banco de dados
Connection String no appsettings.json

🗄️ Migrations (Entity Framework Core)
Navegar até a camada API (startup project):

cd TaskManager.API
Criar migration
bash
Copiar código
dotnet ef migrations add InitialCreate -p ../TaskManager.Infrastructure -s .

Aplicar no banco
dotnet ef database update -p ../TaskManager.Infrastructure -s .

▶️ Executando a Aplicação
dotnet run --project TaskManager.API
A API ficará disponível em:

http://localhost:5042
Swagger

http://localhost:5042/swagger

📡 Endpoints da API
✔️ GET /api/tasks
Retorna todas as tarefas.

✔️ GET /api/tasks/{id}
Retorna uma tarefa pelo ID.

✔️ POST /api/tasks
Cria uma nova tarefa.

✔️ PUT /api/tasks/{id}
Atualiza uma tarefa existente.

Regra automática:

Se status = Completed, o campo CompletedAt é preenchido automaticamente.

Se o status mudar para outro valor, CompletedAt volta a ser null.

✔️ DELETE /api/tasks/{id}
Exclui a tarefa.

🧪 Exemplos de Requisição
POST – Criar Tarefa
{
  "title": "Estudar ASP.NET Core",
  "description": "Revisar Web API",
  "status": 0
}

PUT – Atualizar Tarefa
{
  "title": "Estudar ASP.NET Core",
  "description": "Atualizar para status concluído",
  "status": 2
}

👨‍💻 Autor
Rodrigo de Oliveira Sousa
GitHub: https://github.com/rodrigosousa94
