using almondCoveApi.Data;
using almondCoveApi.Repositories.Implementations;
using almondCoveApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AlmondDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("almondCoveStr"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://laymaann.in", "http://localhost:4200/")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
        });
});




builder.Services.AddMvc();
//repos as services
builder.Services.AddScoped<IMailRepository, MailRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
