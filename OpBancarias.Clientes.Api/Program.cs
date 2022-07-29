using OpBancarias.Clientes.Api;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.Run(app.Configuration.GetSection("ApiConfig").GetValue<string>("ClientesApiUrl"));


