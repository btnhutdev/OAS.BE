using Product.API.Application;
using Product.API.Interfaces;
using Product.API.Repositories;

namespace Product.API.Utilities
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddService();
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            return services;
        }

        private static void AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
        }

        private static void AddService(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IS3BucketService, S3BucketService>();
            services.AddTransient<IS3FileService, S3FileService>();
            services.AddTransient<ISendMailService, SendMailService>();
            services.AddTransient<IHandleProductService, HandleProductService>();
        }
    }
}
