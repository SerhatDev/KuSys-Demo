using FluentValidation;
using FluentValidation.AspNetCore;
using KuSys.Core.AttributeFilters;
using KuSys.Core.Types;
using KuSys.DataAccess.Seed;
using KuSys.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JwtSettings>(o => builder.Configuration.GetSection("JwtSettings").Bind(o));
builder.Services.AddControllers(options =>
{
});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddKuSysServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionMiddleware();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleSeed = scope.ServiceProvider.GetRequiredService<SeedRoles>();
    roleSeed.Seed();
}

app.Run();