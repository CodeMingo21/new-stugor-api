//using Auth0.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Identity.Abstractions;
//using Microsoft.Identity.Web;
//using Microsoft.Identity.Web.Resource;
//using static System.Net.WebRequestMethods;

//namespace StugorAPI
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.

//            builder.Services.AddAuth0WebAppAuthentication(options => {
//                options.Domain = builder.Configuration["Auth0:Domain"];
//                options.ClientId = builder.Configuration["Auth0:ClientId"];
//                options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
//            });

//            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            //    .AddJwtBearer(options =>
//            //    {
//            //        options.Authority = $"https://dev-3pst2842uniw8kh8.eu.auth0.com"/*builder.Configuration["Auth0:Domain"]*/;
//            //        options.RequireHttpsMetadata = true;
//            //        options.Audience = builder.Configuration["Auth0:Audience"];

//            //        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//            //        {
//            //            ValidateIssuer = true,
//            //            ValidIssuer = builder.Configuration["Auth0:Domain"],
//            //            ValidateAudience = true,
//            //            ValidAudience = builder.Configuration["Auth0:Audience"],
//            //            ValidateLifetime = true
//            //        };
//            //    });

//            //.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

//            //builder.Services.AddAuthorization();

//            builder.Services.AddControllers();
//            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new() { Title = "Stugor API", Version = "v1" });

//                // Add JWT Bearer Auth
//                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//                {
//                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
//                    Name = "Authorization",
//                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
//                    Scheme = "bearer"
//                });

//                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
//    {
//        {
//            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//            {
//                Reference = new Microsoft.OpenApi.Models.OpenApiReference
//                {
//                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] { }
//        }
//    });
//            });


//            var app = builder.Build();
//            app.UseRouting();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthentication();
//            app.UseAuthorization();


//            app.MapControllers();

//            app.Run();
//        }
//    }
//}

using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.AspNetCore.Authentication;
using StugorAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Auth0 configuration from appsettings.json
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    options.CallbackPath = new PathString("/signin-auth0");
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MVC
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapDefaultControllerRoute();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // REQUIRED
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
