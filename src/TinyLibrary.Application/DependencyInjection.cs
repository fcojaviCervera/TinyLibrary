using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TinyLibrary.Application.UseCases;
using TinyLibrary.Application.Validators;
using TinyLibrary.Domain.Services;

namespace TinyLibrary.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton(TimeProvider.System);

            //No hay riesgo de condiciones de carrera en LoanService, ya que no mantiene estado mutable compartido entre hilos.
            //Cada operación de préstamo es independiente y no depende de un estado global mutable. Por lo tanto, es seguro registrarlo como singleton.
            services.AddSingleton<LoanService>();

            //Dependen de LibraryDbContext, que es Scoped por defecto, por lo que estos casos de uso también deben ser Scoped.
            services.AddScoped<RegisterBookUseCase>();
            services.AddScoped<RegisterMemberUseCase>();
            services.AddScoped<LendBookUseCase>();
            services.AddScoped<ReturnBookUseCase>();
            services.AddScoped<GetMemberLoansUseCase>();
            services.AddScoped<GetCatalogUseCase>();

            // Agregar validadores de FluentValidation
            services.AddValidatorsFromAssemblyContaining<RegisterBookRequestValidator>();

            return services;
        }
    }
}
