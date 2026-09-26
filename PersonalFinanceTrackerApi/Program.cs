using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Api.ExceptionHandling;
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Application.Services;
using Scalar.AspNetCore;
using PersonalFinanceTracker.Infrastructure.Persistence;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//DB
builder.Services.AddDbContext<FinanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Categories
builder.Services.AddScoped<ICategoryCommandService, CategoryCommandService>();
builder.Services.AddScoped<ICategoryQueryService, CategoryQueryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//Accounts
builder.Services.AddScoped<IAccountCommandService, AccountCommandService>();
builder.Services.AddScoped<IAccountQueryService, AccountQueryService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

//Account Types
builder.Services.AddScoped<IAccountTypeCommandService, AccountTypeCommandService>();
builder.Services.AddScoped<IAccountTypeQueryService, AccountTypeQueryService>();
builder.Services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();

//Transactions
builder.Services.AddScoped<ITransactionCommandService,  TransactionCommandService>();
builder.Services.AddScoped<ITransactionQueryService, TransactionQueryService>();
builder.Services.AddScoped<ITransactionRepository,  TransactionRepository>();

//Transaction Type
builder.Services.AddScoped<ITransactionTypeCommandService, TransactionTypeCommandService>();
builder.Services.AddScoped<ITransactionTypeQueryService, TransactionTypeQueryService>();
builder.Services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();

//Exception Middleware
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// HealthCheck
builder.Services.AddHealthChecks()
    .AddDbContextCheck<FinanceDbContext>();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
