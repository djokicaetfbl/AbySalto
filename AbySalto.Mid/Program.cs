
using AbySalto.Mid;
using AbySalto.Mid.Application;
using AbySalto.Mid.Infrastructure;
using AbySalto.Mid.Infrastructure.Seed;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    var app = builder.Build();

    await RoleSeeder.SeedRolesAsync(app.Services);

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Desk Link");
            options.RoutePrefix = string.Empty;
        });
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowAngularDevClient");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
