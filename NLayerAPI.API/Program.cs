using NLayerAPI.Repository.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using NLayerAPI.Repository.UnitOfWorks;
using NLayerAPI.Core.UnitOfWorks;
using NLayerAPI.Core.Repository;
using NLayerAPI.Repository.Repository;
using NLayerAPI.Core.Services;
using NLayerAPI.Service.Services;
using NLayerAPI.Service.Mapping;
using AutoMapper;
using FluentValidation.AspNetCore;
using NLayerAPI.Service.Validations;
using NLayerAPI.API.Filters;
using Microsoft.AspNetCore.Mvc;
using NLayerAPI.API.Middlewares;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using NLayerAPI.API.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => { options.Filters.Add(new ValidateFilterAttribute());}).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDTOValidator>()); // Kendi validation dönüþ modelimizi tanýmladýk.
builder.Services.Configure<ApiBehaviorOptions>(options =>

 options.SuppressModelStateInvalidFilter = true   // apinin kendi modelini inaktif ettik! 

);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // UnitOfWorks tanýmlandý.
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // GenericRepository tanýmlandý.
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); // Service
builder.Services.AddAutoMapper(typeof(MapProfile)); // Mapping
//builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository)); // ProductRepository tanýmlandý.
//builder.Services.AddScoped(typeof(IProductService),typeof(ProductService));  // ProductService tanýmlandý.
//builder.Services.AddScoped(typeof(ICategoryService),typeof(CategoryService));    // CategoryService tanýmlandý.
//builder.Services.AddScoped(typeof(ICategoryRepository),typeof(CategoryRepository));  // CategoryRepository tanýmlandý.
builder.Services.AddScoped(typeof(NotFoundFilter<>)); // NotFound filter tanýmlandý..


// Baðlantý ayarlarý

builder.Services.AddDbContext<AppDBContext>(x =>
{ 
   x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
   {
       option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDBContext)).GetName().Name);
   });   
});


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // Autofac tanýmlandý.
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException(); // Kendi custom exceptionýmýzý oluþturduk...

app.UseAuthorization();

app.MapControllers();

app.Run();
