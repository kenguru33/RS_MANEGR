using MassTransit;
using Microsoft.EntityFrameworkCore;
using VesselManager.Consumers;
using VesselManager.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
builder.Services.AddDbContext<VesselDbContext>(options =>
{
     options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<StationCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("vessel", false));
    x.AddEntityFrameworkOutbox<VesselDbContext>(o =>
    {
        // check if we have anything in outbox every 10s
        o.QueryDelay = TimeSpan.FromSeconds(10);
        o.UsePostgres();
        o.UseBusOutbox();
    });
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

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