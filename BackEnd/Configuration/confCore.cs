namespace BackEnd.Configuration
{
    public static class confCore
    {
        public static IServiceCollection AddconfCors(this IServiceCollection services)
        {

            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {

                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));


            return services;
        }
    }
}
