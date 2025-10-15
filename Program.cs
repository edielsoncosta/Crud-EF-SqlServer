using TestandoApi.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Inicializa o SqlServer, utilizando a ConnectionString "ConexaoPadrao" definida no appsettings (appsettings.Development.json)
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

// Configura o servidor Kestrel para escutar nas portas HTTP e HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5168); // Porta para HTTP
    options.ListenLocalhost(7253, listenOptions =>
    {
        listenOptions.UseHttps(); // Porta para HTTPS
    });
});

// Configura CORS para permitir chamadas de qualquer origem (ajuste conforme necessidade)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configura os serviços para geração da documentação Swagger
builder.Services.AddEndpointsApiExplorer(); // Necessário para o Swagger detectar endpoints
builder.Services.AddSwaggerGen();           // Gera a documentação Swagger

// Adiciona os serviços de controllers (MVC sem views)
builder.Services.AddControllers();

var app = builder.Build();

// Aplica a política CORS
app.UseCors("AllowAll");

// Habilita o Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Interface gráfica do Swagger
}

app.UseHttpsRedirection(); // Redireciona automaticamente requisições HTTP para HTTPS

// Mapeia os endpoints dos controllers para serem acessíveis pela API
app.MapControllers();

app.Run(); // Inicia a aplicação
