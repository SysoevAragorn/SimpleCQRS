using SimpleCQRS;
using SimpleCQRS.CQRS;

namespace EventBusCQRS {
	public interface IRabbitCommandHandler<TCommand> : ICommandHandler<TCommand>, IHandlerItem, IDispatcherComponent where TCommand : ICommand {
		
	}
}
