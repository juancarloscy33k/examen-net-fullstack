using Examen.NET.Bussiness;
using Examen.NET.Data;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Server=localhost;Database=ExamenNetDB;Trusted_Connection=True;TrustServerCertificate=True;";
builder.Services.AddSingleton(new SqlConn(connectionString));

builder.Services.AddScoped<ClienteData>();
builder.Services.AddScoped<ArticulosData>();
builder.Services.AddScoped<CompraData>();
builder.Services.AddScoped<TiendaData>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IArticuloService, ArticuloService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<ITiendaService, TiendaService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("https://localhost:4200", "http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAngularDev");

app.MapControllers();

app.MapGet("/", () => Results.Content(@"
<!DOCTYPE html>
<html lang=""es"">
<head>
  <meta charset=""UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <title>Servicios API</title>
  <style>
    body {
      background-color: #121212;
      color: #e0e0e0;
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      height: 100vh;
      margin: 0;
      text-align: center;
    }
    h1 {
      font-size: 3rem;
      margin-bottom: 0.2em;
      color: #00bcd4;
      text-shadow: 0 0 8px #00bcd4;
    }
    p {
      font-size: 1.5rem;
      margin: 0.2em 0;
    }
    footer {
      margin-top: 3em;
      font-size: 1rem;
      color: #555;
    }
  </style>
</head>
<body>
  <h1>Servicios para el examen</h1>
  <p>API de gesti&oacute;n de clientes, tiendas y art&iacute;culos.</p>
  <p>Por Juan Carlos Canales Yonca</p>
  <footer>Backend desarrollado en ASP.NET Core</footer>
</body>
</html>
", "text/html"));

app.Run();
