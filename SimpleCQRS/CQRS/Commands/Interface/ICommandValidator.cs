using SimpleCQRS.CQRS;

namespace SimpleCQRS {
	public interface ICommandValidator<in TCommand> : IDispatcherComponent where TCommand : ICommand {
		void Validate(TCommand command);
	}
}
