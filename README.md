[![Build Status](https://travis-ci.org/LUIZFH/desafio-backend.svg?branch=master)](https://travis-ci.org/LUIZFH/desafio-backend)


Desafio Back-end NodeJS - Grupo Fleury - Luiz Filho  

====

  

## Para executar a aplicação:

  

- Deverá ter o **docker** e o **docker-compose** instalados na máquina host

- Clonar o repositório no github

- Acessar a pasta da aplicação no host local

- Para ter a aplicação executando dentro de um container, executar o seguinte comando:

> **docker-compose up -d --build**

- Para manter o terminal preso enquanto executa, rodar:

> **docker-compose up --build**

  

- Opcionalmente, para executar localmente, sem container, deve-se executar os seguintes comandos:

> **npm install**

> **npm run dev**

  

- Apos isso, a API estará disponível para ser consumida. As rotas disponível estão no **Swagger**, que está executando em:

> **http://localhost:4446/documentation**

  

- Para auxiliar, no diretório raiz do projeto tem um diretório **./docs**, que contém um arquivo **challenge-backend.postman_collection.json** com uma collection do postman. Basta importá-lo no **Postman** para ter as rotas prontas para consumir todos os endpoints da API.

  
## Para executar os testes unitaŕios:

- Ter o NodeJS instalado na máquina host

-  Executar os comandos abaixo:
> **npm install**

> **npm run test**


## Requisições usadas para desenvolver e testar:

Na pasta, **./docs**, na raiz do projeto, tem um arquivo **challenge-backend.postman_collection,json**.

Esse arquivo deve ser importado no Postman. Com isso, terá acesso a todas as requests para teste e uso da API.

  
 
## Regras de Negócio

 
- "Não é possível agendar mais de 2 pacientes para o mesmo exame na mesma data e hora"

> **Isso está parametrizado via variavel de ambiente, sendo o default, 2.**

 
## Features

- Deverá haver um endpoint para listagem dos exames disponíveis para agendamento, exibindo apenas nome do exame e id :  *GET /exams*

- Deverá haver um endpoint para criação de um cliente : *POST /customers*

- Deverá haver um endpoint para atualização de um cliente : *PUT /customers*

- Deverá haver um endpoint para exclusão de um cliente : *DELETE /customers*

- Deverá haver um endpoint para busca de um cliente baseado no seu cpf : *GET /customers/{cpf}*

- Deverá haver um endpoint para listagem de todos os clientes cadastrados : *GET /customers*

  
- Deverá haver um endpoint para listagem dos agendamentos de um cliente por cpf, deverá conter o valor total (soma dos valores dos exames selecionados para o agendamento) : *GET /schedules/{cpf}*

- Deverá haver um endpoint para edição de um agendamento realizado, apenas dia e hora poderão ser editados : *PUT /schedules/{id}*

- Deverá haver um endpoint para exclusão de um agendamento realizado : *DELETE /schedules/{id}*

 
## Requisitos

 
- O **swagger** está disponível em **http://{domain}:4446/documentation**

- Os testes unitários cobrem os Serviços *(.src/services)* da aplicação. Eles foram divididos em subpastas com base nas entidades do domínio. Os testes estão na **./tests/unit**. Foi usado o **Jest**.

- Foi criado um objeto *Singleton* em **./data/db** para simular a base de dados em memória, fazendo uso desse *Design Pattern* para gerenciar o estado dos dados.

- O superset **Typescript** foi utilizado no desenvolvimento.


## Diferencial

- Tenha em mente conceitos de **SOLID** e **clean architecture**

- **Docker** e **docker compose**

- Esteira de CI/CD no github: **Travis CI** foi usado para execução dos testes unitários após o push.

## Arquitetura

O servidor da Api foi baseado no framework Web **Express**. A configuração e inicialização do Express está no **./src/app**.

As configurações de servidor e registros de *rotas*, *middlwares* e do *Swagger* ficam no arquivo **./src/app**. Ao final, é exportada uma instância de um servidor.

No **./src/server** esse servidor é importado e iniciado, sendo um ouvinte na **porta**  **4446**.

Os arquivos de variaveis de ambiente estão em **./env/***. O parâmetro do número máximo de agendamentos no mesmo horário, está no arquivo env.

Para resolver a questão da simulação do banco de dados em memória, foi criado um *Singleton* em **./data/db**. O objeto *Singleton* é usado por toda a aplicação, abstraindo o acesso aos dados.

Todo o *domínio/coleções/tabelas* foram estruturado na mesma instancia do objeto *Singleton*, sendo as todas as entidades representadas pelos atributos desse objeto. Essas entidades são acessadas via métodos. Resumindo, essa estrutura foi pensada para facilitar o acesso aos dados, tanto com a aplicação em execucução quanto para execução dos testes unitários.

As collections do postman, usadas para desenvolvimento e testes, estão na pasta ./docs.

Os testes unitários estão em **./tests/unit**. Esse subdiretório *unit* foi criado com a intenção de separar de outros tipos de testes que possam surgir com o andamento do projeto, como testes de integração e *end to end*, por exemplo.

Dentro de **./tests/unit**, os testes foram dividos pensando no domínio, sendo separado em três diretórios: **customer**, **exam** e **schedule**. Os testes cobrem a camada de serviço, sendo o acesso a dados mockado. A chamada a API externa dos exames também foi coberta por teste, sendo a mockado o pacote usado para fazer a requisição. O **TDD** foi aplicado.

O principal diretório da aplicação, onde o domínio foi dividido, pensando em níveis de abstração, é o **./src**. A aplicação foi dividida em *Layers* baseadas em abstração. Conforme a aplicação evolui, tendendo a crescer em tamanho e complexidade, podem ser criadas subcamadas com uma divisão baseadas em recursos. Essa abordagem de divisão por recursos é usada em por outros framework web para APIs. O **Express** "sugere" a divisão usada aqui.

O diretório **./src/http** contém todas as partes para abstrair a manipulação de requisições e respostas para dos clients da API. Essa camada conta com subcamadas, que visam dividar as responsabilidades, deixando-as bem definidas e simples de visualizar. Compoem essa camada os *controllers* e os *middlewares*. Observando que, para este escopo/domínio, não foi implementada nenhum *middlware*. Somente, foram usados *middlwares* nativos do **Express**, que estão configuros e registrados no arquivo de servidor **./src/app**.

Na camada de *controllers*, temos uma divisão por recursos.

- **customerController**: responsável por tratar requisições para o domínio de Clientes/Pacientes.

- **examController**: responsável pelo tratamento de requisições para o domínio de Exames.

- **scheduleController**: trata das interações com o domínio de Agendamento de Exames

  

O arquivo de **./src/http/controllers/index** é usado para centralizar o acesso aos *controllers*. Todos os *controllers* são importados nesse arquivo, que é importado por quem desejar acessar algums dos *controllers*.

  

O diretório de **./src/routes** segue a mesma divisão por recursos dos *controllers*. Um arquivo **index** também foi usado para centralizar o acesso a elas por parte do servidor. Existe um arquivo de rota para cada *controller*.

As responsabilidades dos arquivos de rotas são:

> mapeamento dos controllers para seus domínios

> além de disponiblizar os verbos http que estão disponíves para os recursos.

A camada **./src/providers** foi criada para a interação com a API externa. Nesse diretório tem um único provider Exam. Nele está uma classe para a interação com a API externa. Essa classe possui métodos estáticos para obtenção do conteúdo, visto que é uma classe exclusiva para isso, foi feita a opção por deixar os métodos no escopo de classe, não necessitando do acesso a dados por operações com os objetos. Nessa camada podem ser colocadas classes e/ou funções para interação com pacotes de terceiros, containeres para injeção de dependencia, entre outros.

Na camada de serviço **./src/services** ficam os arquivos que interagem com o banco de dados e com os providers, por exemplo. Essa camada, também, é divida por arquivos separando os recursos. Um **index** é usado para centralizar o acesso.

O tratamento do dado que é enviado na requisição e retornado na resposta é tratado nessa camada, se for uma regra de negócio. Formatação, procurou-se deixar para **helpers** e para a camada mais alta na abstração, os *controllers*.

*Middlewares* poderiam ser usados para interceptar as requisições e as respostas, porém, pelo fato do domínio ser relativamente pequeno, optou-se por adicionar campos nesse fluxo simples.

Uma "camada" de **helpers** foi adicionada. Ela poderia não ser uma camada e ficar fora de src, porém, como todo o domínio está em src, optou-se por mantê-la aqui. Essa camada pode seracessada por qualquer parte do sistema, desde que, faça sentido tal uso. Esse **helper **é usado para formatar a resposta da API externa. Optou-se por dividir essa responsabilidade para deixar o provider mais simples e fácil de manter.

  

A camada de models fica com as interfaces das entidades do negócio. Nela, foram criados arquivos diferentes para cada entidade do domínio, isso para "tipagem" dos objetos trafegados entre as camadas.

Conforme a aplicação cresce, o nível de abstração pode ser elevado, criando interfaces de mais alto nível, e também pode-se trabalhar num nível masi baixo, criando subtipos, dando granularidade, tratando os dados de maneira mas especifica.
Pesa na balança a complexidade versus o controle mais fino dos dados.

Um diretório com as configurações para load das variáveis de ambiente também está em **./src/config**.

Há, na raiz do projeto, um **Dockerfile** para setup do **container docker** para rodar o **NodeJS**. Para a execução do container, foi usado o **docker-compose**. Aqui, é apenas um container para subir, porém, caso seja necessário outros serviços para criar um ecossistema maior, facilitará a adição de outros containeres.

**Travis** foi configurado para executar os testes unitários após o *push*.

  

O **Swagger** executa em **http://{HOST}:4446/documentation**.