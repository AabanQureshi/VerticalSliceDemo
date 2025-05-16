namespace Application.Abstraction
{
    public interface IDispatcher
    {
        Task<TResponse> DispatchCommandHandler<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken)
             where TCommand : ICommand<TResponse>;
        Task<TResponse> DispatchQueryHandler<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken)
            where TQuery : IQuery<TResponse>;
    }
}
