using Microsoft.OpenApi.Models;
using MyBlog.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//�������ע��
builder.Services.AddCors(c => c.AddPolicy("any", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

//Sqlsugar����ע��
builder.Services.AppSqlsugarSetup(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBlog.WebApi", Version = "v1" });
    #region Swaggerʹ�ü�Ȩ���
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�",
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

//����
app.UseCors();

//��Ȩ
app.UseAuthentication();

//��Ȩ
app.UseAuthorization();

app.MapControllers();

app.Run();
