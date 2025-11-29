# Checklist de Desenvolvimento da API de Pedidos e Pagamentos

Este checklist detalha as etapas para o desenvolvimento dos microserviços, seguindo as diretrizes de Clean Architecture, DDD e SOLID, com foco no fluxo principal de Pedidos e Pagamentos.

## Fase 1: Arquitetura e DDD
- [ ] Definir os Bounded Contexts: PedidoContext e PagamentoContext.
- [ ] Listar entidades, Value Objects e eventos para cada contexto (Pedido, ItemPedido, StatusPedido, Evento PedidoCriado; TentativaPagamento, StatusPagamento, Evento PagamentoAprovado/Recusado).
- [ ] Garantir a independência total de domínios (regras, banco, modelo, eventos).

## Fase 2: Estrutura Clean Architecture para Projetos Independentes
- [ ] Criar a solução `APIPedidoMicrosservico`.
- [ ] Criar a estrutura de diretórios e projetos para o **PedidoMicrosservice**, organizando as camadas Clean Architecture: `src/PedidoMicrosservice/Pedido.Application`, `Pedido.Domain`, `Pedido.Infrastructure`, `Pedido.Api`.
- [ ] Criar a estrutura de diretórios e projetos para o **PagamentoMicrosservice**, organizando as camadas Clean Architecture: `src/PagamentoMicrosservice/Pagamento.Application`, `Pagamento.Domain`, `Pagamento.Infrastructure`, `Pagamento.Api`.
- [ ] Desenvolver os modelos de domínio (Entidades, Value Objects, eventos de domínio) na camada `Domain` para ambos os serviços.
- [ ] Definir Use Cases e interfaces de repositórios na camada `Application` para ambos os serviços.
- [ ] Implementar os repositórios concretos e configurações de DB na camada `Infrastructure` para ambos os serviços.
- [ ] Criar Controllers e endpoints REST na camada `Api` para o PedidoMicrosservice (para criação de pedidos).
- [ ] Criar a estrutura da camada `Api` para o PagamentoMicrosservice (foco inicial em health checks, sem endpoints REST para o fluxo principal de pagamentos).

## Fase 3: Aplicação de SOLID
- [ ] Assegurar que cada microserviço siga o Princípio da Responsabilidade Única (SRP).
- [ ] Projetar Use Cases para serem extensíveis sem modificações (Open/Closed Principle - OCP).
- [ ] Validar que as interfaces de repositório respeitam o Princípio da Substituição de Liskov (LSP).
- [ ] Dividir interfaces de repositório para evitar interfaces "gordas" (Interface Segregation Principle - ISP).
- [ ] Implementar a Inversão de Dependência (DIP), garantindo que a camada Application dependa de abstrações.

## Fase 4: Eventos e Mensageria
- [ ] Criar eventos de domínio específicos em `Pedido.Domain.Events` e `Pagamento.Domain.Events`.
- [ ] Configurar um broker de mensagens (RabbitMQ) e uma biblioteca (MassTransit) para publicação/consumo de eventos.

## Fase 5: Implementação do Fluxo End-to-End
- [ ] Desenvolver o fluxo do PedidoMicrosservice (API): Controller -> UseCase -> criação e salvamento de Pedido no repositório.
- [ ] Publicar o `PedidoCriadoEvent` pelo PedidoMicrosservice.
- [ ] Configurar o PagamentoMicrosservice para consumir o `PedidoCriadoEvent` do RabbitMQ.
- [ ] Implementar o Handler de evento no PagamentoMicrosservice para processar o pagamento, criar `TentativaPagamento` e invocar processador externo (simulado).
- [ ] Publicar `PagamentoAprovadoEvent` ou `PagamentoRecusadoEvent` pelo PagamentoMicrosservice.
- [ ] Configurar retry com intervalo e DLQ automática para consumidores do MassTransit.
- [ ] Configurar o PedidoMicrosservice para consumir `PagamentoAprovadoEvent` ou `PagamentoRecusadoEvent`.
- [ ] Implementar o Handler de evento no PedidoMicrosservice para atualizar o status do Pedido.

## Fase 6: Persistência
- [ ] Implementar repositórios de dados separados para PedidoMicrosservice e PagamentoMicrosservice, sem compartilhamento.

## Diagrama de Fluxo (Mermaid)
```mermaid
graph TD
    A[PedidoMicrosservice API] --> B(UseCase Pedido)
    B --> C(Pedido.Domain)
    C --> D(PedidoRepository)
    D -- Salva Pedido --> E[DB Pedido]
    C --> F(Publica PedidoCriadoEvent)
    F --> G[RabbitMQ]

    G --> H(PagamentoMicrosservice Consumidor)
    H --> I(Handler PedidoCriado)
    I --> J(Pagamento.Domain)
    J --> K(TentativaPagamentoRepository)
    K -- Salva TentativaPagamento --> L[DB Pagamento]
    J --> M(Processador Externo Simulado)
    M -- Sucesso/Falha --> N(Publica PagamentoAprovado/RecusadoEvent)
    N --> G

    G --> O(PedidoMicrosservice Consumidor)
    O --> P(Handler PagamentoAprovado/Recusado)
    P --> Q(AtualizarStatusPedidoUseCase)
    Q --> C