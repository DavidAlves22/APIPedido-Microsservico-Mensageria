using MassTransit;
using Pagamento.Application.Consumers;
using Pagamento.Application.Interfaces;
using Pagamento.Application.UseCases;
using Pagamento.Infrastructure.Repositories;
using Pagamento.Infrastructure.EventBus; // Adicionado

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PedidoCriadoConsumer>(); // Registrar o consumidor

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost"); // Configure o host do RabbitMQ

        cfg.ReceiveEndpoint("pedido-criado-queue", e => // Fila para PedidoCriadoEvent
        {
            e.ConfigureConsumer<PedidoCriadoConsumer>(context);

            // Configurar retry com intervalo (3 tentativas, 5 segundos de intervalo)
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
            
            // DLQ automática já é padrão do MassTransit com RabbitMQ
        });
    });
});
builder.Services.AddMassTransitHostedService(); // Adiciona o serviço host para o MassTransit

// Registrar dependências
builder.Services.AddSingleton<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<ProcessarPagamentoUseCase>(); // Registrar o Use Case
builder.Services.AddScoped<IEventBus, MassTransitEventBus>(); // Adicionado

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
