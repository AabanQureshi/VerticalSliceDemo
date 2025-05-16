namespace Application.Abstraction
{
    public interface IQueryHandler<in IQuery, TResponse>
        where IQuery : IQuery<TResponse>
    {
        public Task<TResponse> HandleAsync(IQuery query, CancellationToken cancellationToken);
    }
}
