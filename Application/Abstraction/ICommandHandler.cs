﻿namespace Application.Abstraction
{
    public interface ICommandHandler<in TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
