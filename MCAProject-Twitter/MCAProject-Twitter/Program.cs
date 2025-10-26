using MCAProject_Twitter.Data;
using MCAProject_Twitter.Repositories.Implementation;
using MCAProject_Twitter.Repositories;
using MCAProject_Twitter.Services;
using Microsoft.EntityFrameworkCore;
using MCAProject_Twitter.CQRS.Commands;
using MCAProject_Twitter.CQRS.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MiniTwitterDB"));

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<CreatePostCommandHandler>();
builder.Services.AddScoped<DeletePostCommandHandler>();
builder.Services.AddScoped<RegisterUserCommandHandler>();
builder.Services.AddScoped<GetAllPostsQueryHandler>();
builder.Services.AddScoped<GetMyPostsQueryHandler>();
builder.Services.AddScoped<LoginQueryHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");


app.MapControllers();
app.Run();
