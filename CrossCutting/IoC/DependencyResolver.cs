using System.Reflection;
using Application.CalculateDamage.Haquerin;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.IoC;

public static class DependencyResolver
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // Registrando MediatR e escaneando o assembly correto
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(CalculateHaquerinDamageCommand))!));
    }
}