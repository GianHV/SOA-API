
using Common.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SOA_API.Controllers;
using System.Net;
using System.Text;

namespace SOA_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(options =>
                 {
                     options.RequireHttpsMetadata = false;
                     options.SaveToken = true;
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:key"])),
                         ValidateIssuer = false,
                         ValidIssuer = builder.Configuration["Jwt:Issuer"],
                         ValidateAudience = false,
                         RequireExpirationTime = true,
                         ClockSkew = TimeSpan.Zero,
                     };
                     options.Events = new JwtBearerEvents
                     {
                         OnChallenge = async context =>
                         {
                             context.HandleResponse();

                             // Tạo đối tượng phản hồi
                             var response = new ApiResponse
                             {
                                 StatusCode = HttpStatusCode.Unauthorized,
                                 IsSuccess = false,
                                 ErrorMessages = new List<string> { "You must be authenticated to access this resource" }
                             };

                             // Chuyển đối tượng sang JSON
                             var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

                             // Thiết lập phản hồi
                             context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                             context.Response.ContentType = "application/json";

                             await context.Response.WriteAsync(jsonResponse);
                         }
                     };
                 });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddSwaggerGen(options =>
            {
                // add security definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                        "Example: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                // configure global security requirement
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                        },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

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


            app.MapControllers();

            app.Run();
        }


    }
}
