global using KitchenInventory.Server.Services;
global using KitchenInventory.Server.Models;
global using KitchenInventory.Shared;

using System.Text;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Find the requested path of the database file and create it's containing folder if needed.
var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "KitchenInventory", "data", "KitchenInventory.db");
new FileInfo(dbPath).Directory?.Create();

// Add services to the container.
builder.Services.AddDbContext<ItemContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.JWT));
builder.Services.Configure<AuthCredentialsOptions>(builder.Configuration.GetSection(AuthCredentialsOptions.Credentials));

builder.Services.AddSingleton<IAuthService, AuthService>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Version = "v1",
        Title = "KitchenInventory API",
    });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
// Setup JWT Authentication.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection(JWTOptions.JWT).Get<JWTOptions>()?.Secret ?? throw new Exception("Set the JWT Secret."))),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.MapFallbackToFile("index.html");

// Migrate the database.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<ItemContext>();
    context?.Database.Migrate();
}

app.Run();
