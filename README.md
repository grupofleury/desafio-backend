Desafio Back-end - Grupo Fleury
====

## Descrição:

O Grupo Fleury deseja disponibilizar uma api restfull para realização de agendamentos para seus clientes, 
para tal o usuário precisará ter um cadastro de cliente em nossa base de dados, 
selecionar um exame e informar data e hora desejado.

## Regras de Negócio

- Cliente precisa estar cadastrado em base de dados para realizar o agendamento
- Caso o cliente não exista em base, deverá ser feito o cadastro antecipadamente.
- Não será possível realizar agendamento de mais de 2 pacientes para o mesmo exame na mesma data e hora, esse valor de 2 deverá ser parametrizado.
- O cadastro de cliente deverá ter os campos: Nome, CPF e Data de Nascimento
- Não poderá ser cadastrado mais de um cliente para o mesmo CPF
- A lista de exames disponíveis para agendamento deverá ser consumida da endpoint( http://www.mocky.io/v2/5d681ede33000054e7e65c3f ).

## Features
- Deverá haver um endpoint para listagem dos exames disponíveis para agendamento, exibindo apenas nome do exame e id
- Deverá haver um endpoint para criação de um cliente
- Deverá haver um endpoint para atualização de um cliente
- Deverá haver um endpoint para exclusão de um cliente
- Deverá haver um endpoint para busca de um cliente baseado no seu cpf
- Deverá haver um endpoint para listagem de todos os clientes cadastrados

- Deverá haver um endpoint para listagem dos agendamentos de um cliente por cpf, deverá conter o valor total (soma dos valores dos exames selecionados para o agendamento)
- Deverá haver um endpoint para edição de um agendamento realizado, apenas dia e hora poderão ser editados
- Deverá haver um endpoint para exclusão de um agendamento realizado

## Requisitos

- A API deverá ter um swagger
- Teste unitário
- Utilizar uma estrutura de dados a sua escolha para simular a base de dados em memória
- Para vaga de Back-end NodeJS utilizar Typecript, para Vaga de .net utilizar .net core 2.x
- Aplicação deverá conter um Readme contendo instruções de como realizar o build e rodar a Aplicação


## Diferencial
- No readme separe uma sessão para explicar a arquitetura da api
- Tenha em mente conceitos de SOLID e clean architecture 
- Docker
- Esteira de CI/CD no github (exemplo Travis CI)

## Como submeter?

Deverá ser enviado um PULL REQUEST com o seu teste.

### Como funciona?

- Fork deste repositório
- Clonar a partir do repositório que foi criada na sua conta
- Procure fazer o máximo de commits com todas as suas decisões
- Abra um Pull Request para este repositório

