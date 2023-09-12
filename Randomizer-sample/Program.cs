using Azure.Monitor.OpenTelemetry.AspNetCore;
using InstantAPIs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OTM_sample.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration["Database:ConnectionString"]!));
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "The quote of the day!"
    });
});
builder.Services.AddInstantAPIs(options => options.EnableSwagger = EnableSwagger.Always);

var app = builder.Build();

app.MapGet("/api/quote-of-the-day", (ApplicationDbContext dbContext) =>
{
    var random = new Random();
    if (!dbContext.Quotes.Any()) return Results.NotFound();
    var index = random.Next(0, dbContext.Quotes.Count() - 1);
    var quote = dbContext.Quotes.ToList().ElementAt(index);
    return Results.Ok("Quote of the day is : " + quote.Text);

});

app.MapInstantAPIs<ApplicationDbContext>();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    context?.Database.Migrate();
}

app.Run();