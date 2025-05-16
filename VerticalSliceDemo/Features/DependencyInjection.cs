using Application.Abstraction;
using Domain.ValueTypes;
using VerticalSliceDemo.Features.Product.Create;
using VerticalSliceDemo.Features.Product.Get;
using VerticalSliceDemo.Features.Product.GetById;
using VerticalSliceDemo.Features.Product.Remove;
using VerticalSliceDemo.Features.Product.Update;

namespace VerticalSliceDemo.Features
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services)
        {
            //                          Commands
            services.AddScoped<ICommandHandler<CreateProductCommand, ProductId>, CreateProductCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateProductCommand, ProductId>, UpdateProductCommandHandler>();
            services.AddScoped<ICommandHandler<RemoveProductCommand, RemoveProductCommandResponse>, RemoveProductCommandHandler>();

            //                         Queries
            services.AddScoped<IQueryHandler<GetProductsQuery, IEnumerable<GetProductResponse>>, GetProductsQueryHandler>();
            services.AddScoped<IQueryHandler<GetProductByIdQuery, GetProductResponse>, GetProductByIdQueryHandler>();
            return services;
        }
    }
}
