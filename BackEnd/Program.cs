using BackEnd.Configuration;
using BackEnd.Helpers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllersWithViews()
 .AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


builder.Services.AddAutoMapper(typeof(Program));
//CONFIGURACION SWAGGER
builder.Services.AddConfSwagger();
//CONFIGURACION AUTH
builder.Services.AddConfAuthentication(configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<UserJWT>();
//CONFIGURACION DBCONTEX
builder.Services.AddConfDbContext(configuration);
//CONGURACION CORS
builder.Services.AddconfCors();

//CONFIGURACION RN

builder.Services.AddconfRN();


var app = builder.Build();
app.UseCors();


var valorSwagger = configuration.GetValue<bool>("Swagger:Enabled");
if (valorSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
