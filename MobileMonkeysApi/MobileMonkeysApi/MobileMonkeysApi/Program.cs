using MobileMonkeysApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IMonkeysService, MonkeysService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapGet("/monkeys", async (IMonkeysService monkeysService, CancellationToken cancellationToken) =>
{
    return await monkeysService.GetMonkeys(cancellationToken);
})
.WithName("GetMonkeys");

app.MapPost("/monkeys", async (IMonkeysService monkeysService, Monkey monkey, CancellationToken cancellationToken) =>
{
    return await monkeysService.PostMonkeys(monkey, cancellationToken);
})
.WithName("PostMonkeys");

app.Run();