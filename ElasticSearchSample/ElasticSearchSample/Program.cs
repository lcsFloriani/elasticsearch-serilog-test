using Serilog;
using Serilog.Sinks.Elasticsearch;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .WriteTo.SQLite(sqliteDbPath: builder.Configuration.GetConnectionString("BaseLog"),
                                        tableName: "LogsAPI")
                        .WriteTo.Console()
                        .CreateLogger();

builder.Host.UseSerilog((ctx, lc) => lc
    .Enrich.FromLogContext()
    .WriteTo.Elasticsearch(options: new ElasticsearchSinkOptions(
                                    new Uri(builder.Configuration["Elasticsearch:Uri"]))
    {
        AutoRegisterTemplate = true,
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
        IndexFormat = "logsapi-{0:yyyy.MM}"
    })
    .WriteTo.SQLite(sqliteDbPath: builder.Configuration.GetConnectionString("BaseLog"), tableName: "LogsAPI")
    .WriteTo.Console());

WebApplication? app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Log.Information("Iniciando API");

app.Run();

