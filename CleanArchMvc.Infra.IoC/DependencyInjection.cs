using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
            ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            
            // Loyalty Card System
            services.AddScoped<ILoyaltyCardRepository, LoyaltyCardRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerLoyaltyCardRepository, CustomerLoyaltyCardRepository>();
            services.AddScoped<ILoyaltyCardRedemptionRepository, LoyaltyCardRedemptionRepository>();
            
            services.AddScoped<ILoyaltyCardService, LoyaltyCardService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerLoyaltyCardService, CustomerLoyaltyCardService>();
            services.AddScoped<ILoyaltyCardRedemptionService, LoyaltyCardRedemptionService>();
            
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            var myHandlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
            services.AddMediatR(myHandlers);
            return services;
        }
    }
}