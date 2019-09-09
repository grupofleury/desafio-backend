# Exame

Este projeto possibilita realizar o CRUD do Cliente, listar exames disponiveis para agendamento e efetuar o CRUD de agendamento para o cliente.   

## Pré-Requisitos


```bash
docker
```

ou

```bash
.Net Core 2.2
```

## Buildar utilizando o Docker
Abrir o diretório raiz do projeto e rodar o seguinte comando no terminal
```bash
docker build -t place --build-arg deploy_env='dev' . && docker run -p 8080:8080 place
```

## Documentação
Ao Buildar a aplicação, localhost:{{Porta}}/swagger, exemplo:
```bash
https://localhost:5002/swagger
```

## Postman
O Postman Collection e o Postman Environment estão no diretório
Postman

## Arquitetura da api
A arquitetura conta com o auxilio do DDD(Domain-Driven Design), ajudando na estruturação do projeto, contando também com Entity Framework, AutoMapper, IoC.

## GitHub
[@flaviodmussio](https://github.com/flaviodmussio/exam)
