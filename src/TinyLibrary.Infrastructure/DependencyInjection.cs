using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TinyLibrary.Application.Interfaces;
using TinyLibrary.Infrastructure.Persistence;
using TinyLibrary.Infrastructure.Persistence.Repositories;

namespace TinyLibrary.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IBookRepository, EfBookRepository>();
            services.AddScoped<IMemberRepository, EfMemberRepository>();
            services.AddScoped<ILoanRepository, EfLoanRepository>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            return services;
        }
    }
}
