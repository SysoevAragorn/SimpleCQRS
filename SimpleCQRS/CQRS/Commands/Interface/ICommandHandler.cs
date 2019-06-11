using SimpleCQRS.CQRS;

namespace SimpleCQRS {
	public interface ICommandHandler<TCommand> : IDispatcherComponent where TCommand : ICommand {
		void Handle(TCommand command);
	}
}
