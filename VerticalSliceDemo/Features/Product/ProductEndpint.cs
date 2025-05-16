using VerticalSliceDemo.Features.Product.Create;
using VerticalSliceDemo.Features.Product.Get;
using VerticalSliceDemo.Features.Product.GetById;
using VerticalSliceDemo.Features.Product.Remove;
using VerticalSliceDemo.Features.Product.Update;

namespace VerticalSliceDemo.Features.Product
{
    public static class ProductEndpint
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapCreateProductEndpoint();
            routes.MapGetProductByIdEndpoint();
            routes.MapGetProductsEndpoint();
            routes.MapUpdateProductEndpoint();
            routes.MapRemoveProductEndpoint();
            return routes;
        }
    }
}
