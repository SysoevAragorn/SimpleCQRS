using System.Linq;
using SimpleCQRS.CQRS.Common;

namespace SimpleCQRS {
	public class MessageDispatcher : IMessageDispatcher {
		protected readonly IDispatcherComponentsResolver componentsResolver;

		public MessageDispatcher(IDispatcherComponentsResolver _componentsResolver) => componentsResolver = _componentsResolver;

		public void Publish<TMessage>(TMessage message) where TMessage : IMessage {
			System.Collections.Generic.IEnumerable<IMessageHandler<TMessage>> subscribers = componentsResolver.GetAll<IMessageHandler<TMessage>>();
			if (subscribers != null && subscribers.Any()) {
				foreach (IMessageHandler<TMessage> subscriber in subscribers) {
					subscriber.Handle(message);
				}
			}
		}
	}
}
