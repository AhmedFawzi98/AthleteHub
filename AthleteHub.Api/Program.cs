using AthleteHub.Api.Extensions;
using AthleteHub.Application.Extensions;
using AthleteHub.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddPresentation();


var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
await seeder.SeedAsync();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(CorsPoliciesConstants.AllowAll);
app.UseAuthorization();

app.MapControllers();

app.Run();
