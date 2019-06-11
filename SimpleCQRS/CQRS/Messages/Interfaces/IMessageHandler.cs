using System;
using SimpleCQRS.CQRS;

namespace SimpleCQRS {
	public interface IRabbitMessageHandler<TMessage> : IDisposable, IDispatcherComponent where TMessage : IMessage {

		void Handle(TMessage message);
	}
}
