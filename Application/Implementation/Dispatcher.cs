using Application.Abstraction;

namespace Application.Implementation
{
    public sealed class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> DispatchCommandHandler<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand<TResponse>
        {
            var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResponse>)) as ICommandHandler<TCommand, TResponse> ?? throw new InvalidOperationException("Handler not found");
            return await handler.HandleAsync(command, cancellationToken);
        }

        public Task<TResponse> DispatchQueryHandler<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery<TResponse>
        {
            var handler = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResponse>)) as IQueryHandler<TQuery, TResponse> ?? throw new InvalidOperationException("Handler not found");
            return handler.HandleAsync(query, cancellationToken);
        }
    }
}
