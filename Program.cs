using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using RealEstateListingApi.API.Middlewares;
using RealEstateListingApi.Application.CommandHandlers;
using RealEstateListingApi.Application.Interfaces;
using RealEstateListingApi.Core.Interfaces;
using RealEstateListingApi.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICreateListingCommandHandler, CreateListingCommandHandler>();
builder.Services.AddScoped<IDeleteListingCommandHandler, DeleteListingCommandHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Real Estate Listing API", Version = "v1" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate Listing API V1"));

RunMigrations(app);

app.UseHttpsRedirection();
app.UseTransactionMiddleware();
app.UseAuthorization();
app.MapControllers();
app.Run();

void RunMigrations(WebApplication webApplication)
{
    using var scope = webApplication.Services.CreateScope();
    var services = scope.ServiceProvider;
    using var context = services.GetRequiredService<ApplicationDbContext>();

    if (!context.Database.EnsureCreated())
    {
        context.Database.EnsureDeleted();
        context.Database.Migrate();
    }
}