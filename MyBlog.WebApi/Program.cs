using Microsoft.OpenApi.Models;
using MyBlog.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//跨域服务注册
builder.Services.AddCors(c => c.AddPolicy("any", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

//Sqlsugar服务注册
builder.Services.AppSqlsugarSetup(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBlog.WebApi", Version = "v1" });
    #region Swagger使用鉴权组件
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
                }
            },
            new string[] {}
            }
        });
    #endregion
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//跨域
app.UseCors();

//鉴权
app.UseAuthentication();

//授权
app.UseAuthorization();

app.MapControllers();

app.Run();
