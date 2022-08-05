using Atm.Api.Interfaces;
using Atm.Api.Services;

namespace Atm.Api
{
    public static class ServicesConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IAtmService, AtmService>();
        }
    }
}
