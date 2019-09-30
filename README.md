# Desafio Back-end Grupo Fleury

API Rest para atender o teste proposto.

# Instalação

- Primeiro você deve clonar este diretório da branch master;
- Executar o comando Yarn para instalar todas dependências do projeto ;
- Edite o arquivo .env para configurar o banco de dados (utilize o sqlite caso queira rodar de forma local);
- Edite o parâmetro que define a quantidade de agendadomentos para o mesmo exame, dia e horário através da variável de ambiente MAX_CLIENTS

### Rodando localmente

```
 $ yarn dev
```

### Rodando com o Docker

Para rodar no Docker é necessário fazer o build da imagem que está configurado no Dockerfile, para isso com o Docker instalado execute os seguintes comandos no terminal:

```
  $ docker build -t api .
  $ docker docker run -p 3000:3000 -h 0.0.0.0 api
```

Ou utilize o Docker Compose rodando:

```
  $ docker-compose up -d --build
```

Acesse em: http://localhost:3000/

A aplicação será subida em um container e você poderá acessa-la normalmente.

### Estrutura de diretórios

```
src
│
│
└───__test__
│
│
└───controller
│
│
└───database
│
│
└───helpers
│
│
└───models
│
│
|
└───routes
│
│
└───services
│
│
└───validation
index.ts
server.ts
```

### Testes

Para rodar os testes execute o comando:

```
$ yarn
$ yarn test
```

### Swagger

A documentação está disponivél através da rota:

```
http://localhost:3000/docs
```
