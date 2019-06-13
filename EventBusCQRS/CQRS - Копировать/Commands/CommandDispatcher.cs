using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCQRS.CQRS.Common;

namespace SimpleCQRS.CQRS.Commands {
	public class CommandDispatcher : ICommandDispatcher {
		protected readonly IDispatcherComponentsResolver _componentsResolver;

		public CommandDispatcher(IDispatcherComponentsResolver componentsResolver) => _componentsResolver = componentsResolver;

		public void Execute<TCommand>(TCommand command) where TCommand : ICommand {
			IEnumerable<ICommandValidator<TCommand>> validators = _componentsResolver.GetAll<ICommandValidator<TCommand>>();
			if (validators != null && validators.Any()) {
				foreach (ICommandValidator<TCommand> validator in validators) {
					validator.Validate(command);
				}
			}

			IEnumerable<ICommandHandler<TCommand>> handlers = _componentsResolver.GetAll<ICommandHandler<TCommand>>();
			if (handlers == null || handlers.Count() == 0) {
				throw new InvalidOperationException(string.Format("Command handler for {0} not found", command.GetType().Name));
			}
			ICommandHandler<TCommand> handler = handlers.Single();
			handler.Handle(command);
		}
	}
}
