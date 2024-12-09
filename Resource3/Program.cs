
using System.Net;
using System.Text;
using Common.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Resource3.Middleware;
using Resource3.Services.External;
using Resource3.Services.Internal;

namespace Resource3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddHttpClient<IProductService, ProductService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderReportService, OrderReportService>();
            builder.Services.AddScoped<IProductReportService, ProductReportService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

                           var response = new ApiResponse
                           {
                               StatusCode = HttpStatusCode.Unauthorized,
                               IsSuccess = false,
                               ErrorMessages = new List<string> { "You must be authenticated to access this resource" }
                           };

                           var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

                           context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                           context.Response.ContentType = "application/json";

                           await context.Response.WriteAsync(jsonResponse);
                       }
                   };
               });

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
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<TokenMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
