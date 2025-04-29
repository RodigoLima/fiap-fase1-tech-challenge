# fiap-fase1-tech-challenge

API responsável pelo controle da plataforma de venda de jogos e servidor de partidas online FIAP Cloud Games.

## Database

### Configuração

Este projeto utiliza o banco de dados PostgreSQL. Para realizar a conexão, deve ser adicionado os dados da conexão no arquivo appsettings.json, seguindo o seguinte exemplo:

```json
  "ConnectionStrings": {
    "DefaultConnection": "User ID=<USUARIO>;Password=<SENHA>;Host=<HOST>;Port=5432;Database=<DATABASE>;Pooling=true;"
  },
```

### Migração

Para a conexão com o banco e controle das tabelas, foi utilizado o Entity Framework Core. Para realizar a criação das tabelas conforme as migrations disponibilizadas, deve ser executado o seguinte comando:

```sh
    dotnet ef database update
```