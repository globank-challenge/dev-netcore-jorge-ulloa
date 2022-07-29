using OpBancarias.Movimientos.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddEventLog();


var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.Run(app.Configuration.GetSection("ApiConfig").GetValue<string>("MovimientosApiUrl"));


