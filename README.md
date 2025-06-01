# Fiap Cloud Games

Uma API criada em Dot Net Core 8 responsável pelo controle da plataforma de venda de jogos e servidor de partidas online da FIAP. Este projeto oferece endpoints para administrar usuários, Jogos, Promoções, Biblioteca de Jogos e Papéis.

## Indice

- [Instalação](#instalação)
  - [Download](#download)
  - [Configurações](#configurações)
- [Uso](#uso)
  - [Url Endpoints](#url-endpoints)
  - [Controle de acesso](#controle-de-acesso)
  - [Bearer](#bearer)
- [Swagger](#swagger)
- [Testes](#testes)
- [Contribuição](#contribuição)

## Instalação

Para realizar a instalação deste projeto, alguns passos devem ser seguidos.

### Download

Primeiramente, deve ser realizado o download (clone) deste repositório do GitHub. Este clone pode ser realizado através do seguinte comando:

```bash
  git clone https://github.com/RodigoLima/fiap-fase1-tech-challenge.git
```

### Configurações

Após clonar o projeto, algumas configurações são necessárias no arquivo AppSettings.json.

#### Database (PostgreSQL)

Este projeto utiliza o banco de dados PostgreSQL. Para realizar a conexão, deve ser adicionado os dados da conexão no arquivo appsettings.json, seguindo o seguinte exemplo:

```json
  "ConnectionStrings": {
    "DefaultConnection": "User ID=<USUARIO>;Password=<SENHA>;Host=<HOST>;Port=5432;Database=<DATABASE>;Pooling=true;"
  },
```

Ao rodar o projeto, automaticamente serão executadas todas as migrations e seeds do banco de dados. Para que isso aconteça corretamente, além do banco de dados estar com sua connection string corretamente configurada, conforme exemplo acima, o banco de dados deve estar disponível.

#### Token JWT

Para realizar a autenticação na plataforma, é necessário o envio de um Token JWT em todas as rotas da aplicação. Para que o sistema consiga gerar corretamente este token, algumas configurações são necessárias no arquivo appsettings.json. Essas configurações podem ser vistas a seguir:

```json
  "Jwt": {
    "Key": "QRl/XPT7lvtFeR51JSP+13l8Wt/BGWHA7p/wqcDshxjXVlPJ/WdIc75U9ceCLwz6ffdHRR+FlUgSelz9tEZ+nA==", //Chave em Base64.
    "Issuer": "fgc-api",
    "Audience": "fgc-client",
    "AccessTokenExpirationMinutes": 15, //Tempo de expiração do token em minutos.
    "RefreshTokenExpirationDays": 7 //Tempo de expiração do refresh token em dias.
  },
```

#### Logs

Esta aplicação está preparada para utilizar o Serilog para gerar os logs. Para isso, devem ser adicionadas as seguintes configurações no appsettings.json

```json
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning", // ou "Warning" se quiser menos verbosidade
        "System": "Warning"
      }
    },
    "WriteTo": [
      { "Name": "Console" },
      {
        "Name": "File",
        "Args": {
          "path": "Logs/log-.txt",
          "rollingInterval": "Day"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
  }
```

## Uso

Abaixo algumas explicações sobre a utilização desta API. Maiores detalhes podem ser consultados através do Swagger disponibilizado.

### Url Endpoints

Todos os endpoints da API possuem como padrão a url "http://url-da-api/api/endpoint". Através desses endpoints será possível realizar o controle de cada uma das informações disponíveis na aplicação.

### Controle de acesso

Para o acesso a qualquer rota desta API, é obrigatório que o usuário esteja previamente logado (desconsiderando a rota de autenticação, que permite o acesso externo).

Ainda existem algumas rotas da API que, além de obrigar que o usuário esteja logado, também obriga que o usuário seja um administrador. Para o uso inicial, foi disponibilizado um usuário administrador padrão que poderá ser acessado com os seguintes dados:

email: adm@fcg.com  
password: @AdminFCG1234

### Bearer

Para o envio do token nas rotas, deve ser enviado os dados no padrão Bearer {token}

## Swagger

Esta aplicação foi desenvolvida utilizando o Swagger como documentação. Para acessar a documentação, basta acessar a URL http://url-da-api/docs

## Testes

Ao clonar o repositório desta API, um projeto "Tests" será baixado também. Neste projeto ficarão todos os testes automatizados da aplicação, que poderão ser configurados para executar em uma pipeline.

## Contribuição

Esta API foi desenvolvida por:

- [Leonardo Dick Bernardes](http://github.com/oleonardodick)
- [Rodrigo Ferreira](https://github.com/RodigoLima)
- [Renato Ventura](http://github.com/renydev)
