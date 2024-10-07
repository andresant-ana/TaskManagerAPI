# TaskManager API

## Descrição

O **TaskManager API** é um projeto desenvolvido em .NET 8 que fornece uma API RESTful para gerenciar tarefas. Ele utiliza MongoDB como banco de dados e segue as melhores práticas de desenvolvimento, incluindo testes unitários com xUnit e Moq.

## Tecnologias Utilizadas

- **.NET 8**
- **C# 12.0**
- **MongoDB**
- **xUnit** para testes unitários
- **Moq** para mocks nos testes
- **Swagger** para documentação da API

## Estrutura do Projeto

### Diretórios Principais

- **TaskManager.API**: Contém a implementação da API.
- **TaskManager.Tests**: Contém os testes unitários.

### Arquivos Importantes

- **appsettings.json**: Configurações gerais da aplicação.
- **appsettings.Development.json**: Configurações específicas para o ambiente de desenvolvimento.
- **Program.cs**: Ponto de entrada da aplicação.
- **Startup.cs**: Configuração dos serviços e middlewares da aplicação.
- **TaskManager.API.csproj**: Arquivo de projeto da API.
- **TaskManager.Tests.csproj**: Arquivo de projeto dos testes.

## Endpoints

### Tarefas

- **GET /api/tarefas**: Retorna todas as tarefas.
- **GET /api/tarefas/{id}**: Retorna uma tarefa específica pelo ID.
- **POST /api/tarefas**: Cria uma nova tarefa.
- **PUT /api/tarefas/{id}**: Atualiza uma tarefa existente.
- **DELETE /api/tarefas/{id}**: Remove uma tarefa pelo ID.

## Configuração

### Banco de Dados

A configuração do MongoDB é feita no arquivo `appsettings.Development.json`:

```
{
  "DatabaseConfig": {
    "DatabaseName": "taskManager",
    "ConnectionString": "mongodb+srv://<username>:<password>@<cluster-url>/taskManager?retryWrites=true&w=majority"
  }
}
```

### Dependências

As dependências do projeto estão listadas nos arquivos `.csproj`:

**TaskManager.API.csproj**:

```
<ItemGroup>
  <PackageReference Include="MongoDB.Driver" Version="2.29.0" />
  <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
</ItemGroup>
```

**TaskManager.Tests.csproj**:

```
<ItemGroup>
  <PackageReference Include="coverlet.collector" Version="6.0.0" />
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
  <PackageReference Include="Moq" Version="4.20.72" />
  <PackageReference Include="xunit" Version="2.9.2" />
</ItemGroup>
```

## Executando a Aplicação

1. **Clone o repositório**:
   
```
git clone https://github.com/seu-usuario/taskmanager-api.git
cd taskmanager-api
```

2. **Configure o MongoDB**:
   Atualize a string de conexão no arquivo `appsettings.Development.json`.

3. **Execute a aplicação**:
   
```
dotnet run --project TaskManager.API
```

4. **Acesse a documentação da API**:
   Abra o navegador e vá para `http://localhost:5213/swagger`.

## Executando os Testes

Para executar os testes unitários, utilize o comando:

```
dotnet test
```