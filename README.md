Este projeto implementa um sistema de processamento de pedidos e pagamentos utilizando uma arquitetura de microsserviços. Cada microsserviço (PedidoMicrosservice e PagamentoMicrosservice) é estruturado seguindo o padrão Clean Architecture, garantindo uma clara separação de responsabilidades entre as camadas.

O objetivo é demonstrar uma solução robusta e escalável, com foco em:

*   **Escalabilidade:** Capacidade de lidar com um volume crescente de transações de pedidos e pagamentos de forma independente.
*   **Testabilidade:** Facilidade na criação de testes unitários e de integração devido à arquitetura modular e desacoplada.
*   **Manutenibilidade:** Código organizado e com baixo acoplamento, facilitando futuras alterações e evoluções.

**Tecnologias Utilizadas:**

- ASP.NET Core Web API
- Banco de dados: Não possui um banco real, apenas uma lista no repositório que representa o banco.
- Clean Architecture (Domain, Application, Infrastructure, API)
- Microsserviços
- C#:
- RabbitMQ - MassTransit: Para comunicação assíncrona entre os microsserviços via Event Bus.
