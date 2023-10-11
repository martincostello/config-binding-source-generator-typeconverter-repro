using Microsoft.Extensions.Options;
using Repro;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
builder.Services.Configure<SiteOptions>(builder.Configuration.GetSection("Site"));

var app = builder.Build();

_ = app.Services.GetRequiredService<IOptions<SiteOptions>>().Value;

app.Run();
