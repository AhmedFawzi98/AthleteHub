using AthleteHub.Api.Extensions;
using AthleteHub.Application.Extensions;
using AthleteHub.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddPresentation();


var app = builder.Build();

app.UseExceptionHandler(_ => { });

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
await seeder.SeedAsync();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseSerilogRequestLogging(options =>
{
    options.GetLevel = (httpContext, _, _) =>
    {
        int statusCode = httpContext.Response.StatusCode;
        if (statusCode >= 100 && statusCode <= 399)
        {
            return LogEventLevel.Information;
        }
        return LogEventLevel.Error;
    };
});

app.UseRouting();

app.UseCors(CorsPoliciesConstants.AllowAll);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
