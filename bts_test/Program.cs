using bts_test.Services;
using bts_test.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SqlLiteConnection>();

builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<SqlLiteQuery>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    options => 
                        options.TokenValidationParameters = new TokenValidationParameters {
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    builder.Configuration.GetSection("AppSettings:Key").Value
                                )
                            )
                        }
                );

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var sqlLiteConnection = scope.ServiceProvider.GetRequiredService<SqlLiteConnection>();
    sqlLiteConnection.CreateTable();
    sqlLiteConnection.InsertDummyData();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
