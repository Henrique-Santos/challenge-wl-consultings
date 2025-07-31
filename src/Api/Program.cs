using Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    builder.Services.AddSwaggerConfigurations();

    builder.Services.AddIdentityConfigurations(builder.Configuration);

    builder.Services.AddJwtConfigurations(builder.Configuration);

    builder.Services.AddDatabaseConfigurations(builder.Configuration);

    builder.Services.AddDependencyInjectionConfigurations();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}