# fiap-fase1-tech-challenge

API respons�vel pelo controle da plataforma de venda de jogos e servidor de partidas online FIAP Cloud Games.

## Database

### Configura��o

Este projeto utiliza o banco de dados PostgreSQL. Para realizar a conex�o, deve ser adicionado os dados da conex�o no arquivo appsettings.json, seguindo o seguinte exemplo:

```json
  "ConnectionStrings": {
    "DefaultConnection": "User ID=<USUARIO>;Password=<SENHA>;Host=<HOST>;Port=5432;Database=<DATABASE>;Pooling=true;"
  },
```

### Migra��o

Para a conex�o com o banco e controle das tabelas, foi utilizado o Entity Framework Core. Para realizar a cria��o das tabelas conforme as migrations disponibilizadas, deve ser executado o seguinte comando:

```sh
    dotnet ef database update
```