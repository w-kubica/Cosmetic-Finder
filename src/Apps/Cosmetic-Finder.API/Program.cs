using Cosmetic_Finder.Application.Services;
using Cosmetic_Finder.Core.Repositories;
using Cosmetic_Finder.Infrastructure.Data;
using Cosmetic_Finder.Infrastructure.DTO;
using Cosmetic_Finder.Infrastructure.Repositories;
using SolrNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ICosmeticService, CosmeticService>();
builder.Services.AddTransient<ICosmeticRepository, CosmeticRepository>();

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();

builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<ITagService, TagService>();

builder.Services.AddSolrNet<SolrCosmetic>("http://localhost:8983/solr/cosmetics");

builder.Services.AddDbContext<CosmeticFinderContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .SetIsOriginAllowed(_ => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
