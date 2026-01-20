using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.Authority = "http://keycloak.local:8888/realms/tfe";
      options.RequireHttpsMetadata = false;

      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidIssuer = "http://keycloak.local:8888/realms/tfe",
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true
      };
  });

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.MapGet("/hello", () =>
{
    return "Hello, World!";
}).RequireAuthorization();

app.MapGet("/test", () =>
{
    return "Test!";
}).AllowAnonymous();

app.Run();
