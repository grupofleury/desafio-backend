# Schedule API
[![Build Status](https://travis-ci.org/JeffersonApolinario/desafio-backend.svg?branch=master)](https://travis-ci.org/JeffersonApolinario/desafio-backend)

Neste repositório contem um exemplo de API de agendamento de exames, ela consiste em um CRUD para realizar agendamentos de exames para um cliente.

## Guia
- Estrutura do projeto
- Configuração
- Execução
- Execução dos testes
- Documentação da API

## Estrutura do projeto
  ```
    /migrations # pasta que contem os arquivos de migração
    /__tests__ # pasta que contem os arquivos de test
    /src # pasta que contem o source do projeto
    /src/controllers # pasta que contem as controllers
    /src/entitites # pasta que contem as entidades do projeto
    /src/integrations # pasta que contem arquivos que executam chamadas externas
    /src/server # pasta que contem o http server da API
    /src/validators # pasta que contem schemas de validação e classes de validação

    index.ts # arquivo que inicia a aplicação
    .dockerignore # arquivo de configuração para ignorar arquivos de build do docker
    .eslinrc # arquivo de configuração do eslint, ferramenta de padronização de código
    Dockerfile # arquivo de configuração para build da imagem docker
    ormconfig.yml # arquivo de configuração para o orm
    prettierrc.yml # arquivo de configuração do prettier, ferramenta de padronização de código
    deploy.sh # script para rodar e deploy a aplicação
    tsconfig.json # arquivo de configuração de build e desenvolvimento do typescript
    swagger.yml # arquivo con a documentação da API
  ```

## Configuração
Neste projeto contem variáveis de ambiente, para configura-la altere o arquivo de deploy.sh, as variáveis são:

- EXAM_URL
- DATABASE_NAME

Exemplo:

```
EXAM_URL=http://www.mocky.io/v2
DATABASE_NAME=dataset.db
```

## Execução
Este projeto pode ser executado localmente ou em um container docker, para executar localmente devera ter instalado o Node 8+. Para executar siga as instruções:

Local:
```
npm i
./deploy.sh local
```

Docker:
```
.deploy.sh dev
```

## Execução dos testes
Este projeto utiliza o jest como framework de teste, pode ser executado de duas maneiras que são com relatório ou sem, se executar com o relatório irá aparecer uma pasta chamada coverage, ela irá conter o relatório de cobertura em HTML. Para executar siga as instruções:

Sem relatório:
```
npm test
```

Com relatorio:
```
npm run test:coverage
```

## Documentação da API
Este projeto contem a documentação da API em swagger, que se encontra no arquivo swagger.yml, caso queria visualiza-lo realize a importação do conteudo no swagger editor