using Application.Abstraction;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace VerticalSliceDemo.Features.Product.Get
{
    public sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<GetProductResponse>>
    {
        private readonly ApplicationDBContext _Context;

        public GetProductsQueryHandler(ApplicationDBContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<GetProductResponse>> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _Context.Products
                            .AsNoTracking()
                            .Select(p => new GetProductResponse
                            {
                                Name = p.Name,
                                Description = p.Description,
                                Price = p.Price,
                                Stock = p.Stock
                            }).ToListAsync(cancellationToken);
            return products;
        }
    }
}
