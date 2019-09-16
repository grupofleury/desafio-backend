## Desafio Backend

Esse projeto contém uma estrutura desenvolvido em .Net Core com arquitetura limpa/hexagonal.

A Arquitetura Hexagonal (também conhecida como Portas e Adaptadores) é uma estratégia para dissociar os casos de uso dos detalhes externos. Foi inventado por Alistar Cockburn há mais de 13 anos. 

Ao longo do tempo outros engenheiros trabalharam em veriaçoes dela:
- Onion Architecture - Jeffrey Palermo https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/
- Clean Architecture - Uncle Bob.

Embora estes arquetipos variem em algum ou outro detalhe, todos possuem o mesmo objetivo: 

> Separar a logica da aplicaçãode detalhes externos.


Regras de Negocios e os Casos de Uso são ser implementados dentro da Camada Core (Aplicaçãoo e Domínio) e são mantidas em toda a vida do produto.  


Po
1) Entrypoint (Projeto(s) com WebAPI, listeners de filas, worker de um job agendado, etc.) Não deve ter  regras de negócio nem de aplicação. Apenas obtém as entradas e delega para a camada de aplicaçãoo. Pode executar alguma formatação na saída para o formato apropriado do seu consumidor.
2) Core - bibliotecas sem dependencias de frameworks com o principal da aplicaçãoo: as Regras de Negócio e Caso de Uso.
   Aqui dividimos em duas camadas: 
   - Domain - onde ficam as regras de negócio (em tempos de DDD também chamadas de domínio e expressadas através de  entidades, serviçoos de dominio, value objects, interfaces de repositórios, etc.) 
     Gosto bastante da definiçãoo do Uncle Bob para a "camada" de domínio:
    > São as regras de negócios que fazem ou economizariam o dinheiro da empresa, independentemente de terem sido implementadas em um computador. Elas fariam ou economizariam dinheiro mesmo se fossem executadas manualmente.
  - Application  - onde iremos implementar as regras específicas da aplicação

3) Infrastructure - aqui são as implementaçõees contretas para todos os "detalhes". A camada que conhece frameworks e bits da aplicaçãoo como qual banco de dados, qual ORM, filtros e middlewares de Web API, etc.


### Projetos:

No desenvolvimento da solução foram utilizados os seguintes targets de compilação:

- Bibliotecas - .Net Standard 2.0
- Entrypoint\Web API - .Net Core 2.2
- Tests - .Net Core 2.0 / xUnit

### Base de dados
- Base de dados em memória

### Documentaçaoo de API 

Foi utizado o pacote [SwashBuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) para gerar a documentaçãoo da API no formato OpenAPI / Swagger. Para explorar a API, acesse https://localhost:5000/swagger.

As configurações podem ser encontradas no projeto `Fleury.Agendamento.Infrastructure.Bootstrap.Infrastructure.Bootstrap`, nos arquivos `/ApplicationBuilder/SwaggerApplicationBuilderExtensions.cs` e  `/ServiceCollection/SwaggerServiceCollectionExtensions.cs`.


### HealthChecks
Utilizamos a biblioteca Microsoft.AspNetCore.Diagnostics.HealthChecks - https://docs.microsoft.com/pt-br/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-2.2.
A configuração esté disponível em `Fleury.Agendamento.Infrastructure.Bootstrap.Infrastructure.Bootstrap` nos arquivos `/ApplicationBuilderExtensions/HealthChecksApplicationBuilderExtensions` e `/ServiceCollectionExtensions/HealthChecksServiceCollectionExtensions`.

http://localhost:5000/health

### Testes Unitários

Os testes unitários foram escritos usando XUnit e as seguintes bibliotecas:
- FluentAssertions - https://fluentassertions.com/ - para asserts mais expressivos
- Moq - https://github.com/Moq/moq4/wiki/Quickstart - para mocks


### Buid e execução a aplicação


Buid - Acessar a pasta da aplicação e rodar o comando

dotnet build

Execução - Acessar o Projeto API e executar o comando 
dotnet run

Obter a porta na linha de comando e realizar os test atráves do Postman

Anexo o projeto


