using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBusCQRS.ConnectionManager;
using SimpleCQRS;
using SimpleCQRS.CQRS.Common;

namespace EventBusCQRS.CQRS {
	public class RabbitCommandDispatcher : ICommandDispatcher {
		protected readonly IDispatcherComponentsResolver _componentsResolver;
		protected readonly IRabbitMQConnection _rabbitMQConnection;

		public RabbitCommandDispatcher(IDispatcherComponentsResolver componentsResolver, IRabbitMQConnection rabbitMQConnection) {
			_componentsResolver = componentsResolver;
			_rabbitMQConnection = rabbitMQConnection;
		}

		public void Execute<TCommand>(TCommand command) where TCommand : ICommand {
			IEnumerable<ICommandValidator<TCommand>> validators = _componentsResolver.GetAll<ICommandValidator<TCommand>>();
			if (validators != null && validators.Any()) {
				foreach (ICommandValidator<TCommand> validator in validators) {
					validator.Validate(command);
				}
			}

			_rabbitMQConnection.PublishToQueue(command.GetType().Name, command);
		}
	}
}
