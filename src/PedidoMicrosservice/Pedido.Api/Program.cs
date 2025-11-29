using MassTransit;
using Pedido.Application.Consumers; // Adicionado
using Pedido.Application.Interfaces;
using Pedido.Application.UseCases;
using Pedido.Infrastructure.EventBus;
using Pedido.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PagamentoAprovadoConsumer>(); // Registrar o consumidor
    x.AddConsumer<PagamentoRecusadoConsumer>(); // Registrar o consumidor

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost"); // Configure o host do RabbitMQ

        cfg.ReceiveEndpoint("pagamento-aprovado-queue", e => // Fila para PagamentoAprovadoEvent
        {
            e.ConfigureConsumer<PagamentoAprovadoConsumer>(context);
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        });

        cfg.ReceiveEndpoint("pagamento-recusado-queue", e => // Fila para PagamentoRecusadoEvent
        {
            e.ConfigureConsumer<PagamentoRecusadoConsumer>(context);
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        });
    });
});
builder.Services.AddMassTransitHostedService();

// Registrar dependências
builder.Services.AddSingleton<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<CriarPedidoUseCase>();
builder.Services.AddScoped<AtualizarStatusPedidoUseCase>(); // Registrar o Use Case
builder.Services.AddScoped<IEventBus, MassTransitEventBus>();

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
