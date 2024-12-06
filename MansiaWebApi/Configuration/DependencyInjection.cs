namespace MansiaWebApi.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        //public static IServiceCollection AddAuthentictionAndAuthorization(this IServiceCollection services)
        //{

        //}
        
    }
}
