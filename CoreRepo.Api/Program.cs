using CoreRepo.Api.Profile;
using CoreRepo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using TanvirArjel.EFCore.GenericRepository;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(_ =>
{
    _.DefaultApiVersion = new ApiVersion(1, 0);
    _.AssumeDefaultVersionWhenUnspecified = true;
    _.ReportApiVersions = true;
    _.ApiVersionReader = new HeaderApiVersionReader("api-version");
});

builder.Services
    .AddDbModule(builder.Configuration)
    .AddRepository();

builder.Services.AddGenericRepository<CoreDbContext>();
builder.Services.AddQueryRepository<CoreDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//AutoMapper
builder.Services.AddAutoMapper(typeof(ReceiptProfile));

#endregion

#region App
var app = builder.Build();

app
    .UseDbModule()
    .UseFakeData();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
