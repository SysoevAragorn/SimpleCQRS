namespace SimpleCQRS {
	public interface IMessageDispatcher {
		void Publish<TMessage>(TMessage message) where TMessage : IMessage;
	}
}
