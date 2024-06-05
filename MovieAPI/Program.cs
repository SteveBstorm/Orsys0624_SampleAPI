using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieAPI.Hubs;
using MovieAPI.Infrastructure;
using MovieAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddSingleton<MovieService>();
builder.Services.AddScoped<TokenManager>();
builder.Services.AddScoped<UserService>();

#region Authentification
/*using Microsoft.AspNetCore.Authentication.JwtBearer;*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenManager._secretkey)),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = false
            };
        }
    );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminPolicy", policy => policy.RequireRole("admin"));
    options.AddPolicy("userPolicy", policy => policy.RequireAuthenticatedUser());
});
#endregion

builder.Services.AddCors(o => o.AddPolicy("myPolicy", options =>
/*NE PAS METTRE LE DERNIER /  */
    options.WithOrigins("https://localhost:7085")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("myPolicy");
app.UseHttpsRedirection();
//app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("chathub");

app.Run();
