<h1 align="center">FastFood API</h1>

<p align="center">
  <a href="#sobre">Sobre</a>&nbsp;&nbsp;|&nbsp;&nbsp;
  <a href="#tecnologias">Tecnologias</a>&nbsp;&nbsp;|&nbsp;&nbsp;
  <a href="#documentação">Documentação</a>&nbsp;&nbsp;|&nbsp;&nbsp;
  <a href="#instalação">Instalação</a>
</p>

<p align="center">
  <a href="#-license">
    <img alt="License" src="https://img.shields.io/static/v1?label=license&message=MIT&color=24d67d&labelColor=000000">
  </a>
</p>

## 📃 Sobre

O FastFood API entrega uma solução de auto-atendimento para restaurantes que gerencia todo o fluxo de pedidos, desde sua criação, pagamento, preparo, monitoramento e entrega ao cliente.

Esse projeto é o tech challenge proposto pela pós-graduação de Arquitetura de Software da FIAP. 
Ao longo dos próximos meses a solução receberá evoluções graduais, por esse motivo, ainda não está com todas as funcionalidades esperadas. 

## 🚀 Tecnologias

A tecnologias utilizadas nesse projeto foram escolhidas com o objetivo de entregar uma solução que seja escalável, resiliente e de fácil manutenção. 
Pensando nos passos futuros, foi optado por seguir uma **arquitetura monolítica modular**, que facilitará a migração para microsserviços. 
Os contextos delimitados levantados no processo de documentação do DDD, viram módulos dentro da solução, que podem ser facilmente extraídos para microsserviços.

Os conceitos e tecnologias utilizadas no projeto foram:
- Domain Driven Design (DDD)
- Arquitetura Hexagonal e Arquitetura Limpa
- .NET Core 7
- EF Core
- Postgres
- Docker
- Command Query Segregation (CQS) com MediatR
- Minimals API
- xUnit
- Kubernetes

## 📖 Documentação

Nessa solução estamos utilizando o Domain Driven Design (DDD) para entender e modelar o domínio do negócio, identificando e categorizando os subdomínios.

Como documentação, foram criados alguns artefatos para facilitar o entendimento da equipe, sendo eles:
- [Glossário da Linguagem Ubíqua](/docs/linguagem-ubiqua/glossario.md)
- Domain Storytelling
  - [Fluxo de pedido](/docs/storytelling/fluxo-pedido.png)
  - [Fluxo de pedido - Pagamento reprovado](/docs/storytelling/fluxo-pedido-pagamento-reprovado.png)
  - [Fluxo de preparo](/docs/storytelling/fluxo-preparo.png)
- [Context Map](/docs/context-map/src-gen/context-map_ContextMap.png)
- [Event Storming](https://miro.com/welcomeonboard/bFNEM05zRk1oT0R6THE0VnNTaTVEOGQ2Z0NQbkM1SWxxQ0R6dk9qTHlrUzhlRDhhUEhTUTg1U21INXFhMTFTNXwzNDU4NzY0NTU5OTUzNzM4OTIzfDI=?share_link_id=357172007426)
- C4 Model
  - [Context](/docs/c4-model/system-context.png)
  - [Container](/docs/c4-model/container.png)
  - [Component](/docs/c4-model/component.png)
- Kubernetes
  - [Diagrama](/docs/k8s/diagrama.png)
- Requests (API)
  - Swagger: http://localhost/swagger
  - [Postman para importar](postman.json)

## 💻 Instalação

**Premissas:**
- Ter o docker instalado na maquina.

Para executar a aplicação siga o passo a passo a seguir.

Realizar o clone do repositório na sua maquina:

```bash
git clone https://github.com/jonasaraldi/FastFood.git
```

Executar o docker compose:

```bash
docker compose up --build -d
```

Acessar no navegador:
http://localhost/swagger


Caso queira debugar o projeto, é importante ter instalado o [SDK do .NET Core 7](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0) e algum editor/IDE como:
- Visual Studio Code
- Visual Studio
- JetBrains Rider

## Kubernetes

Definindo contexto
```bash
kubectl config set-context --current --namespace=fastfood
```

Rodando os YAMLs
```bash
kubectl apply -f k8s
kubectl apply -f k8s/postgres
kubectl apply -f k8s/api
```


