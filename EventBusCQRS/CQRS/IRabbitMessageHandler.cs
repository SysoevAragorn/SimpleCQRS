using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCQRS;
using SimpleCQRS.CQRS;

namespace EventBusCQRS {
	public interface IRabbitMessageHandler<TMessage> : IHandlerItem, IDisposable, IDispatcherComponent where TMessage : IMessage {
		void Handle(TMessage message);
	}
}
