using SimpleCQRS;
using SimpleCQRS.CQRS;

namespace EventBusCQRS {
	public interface IRabbitCommandHandler<TCommand> : IHandlerItem, IDispatcherComponent where TCommand : ICommand {
		void Handle(TCommand command);
	}
}
