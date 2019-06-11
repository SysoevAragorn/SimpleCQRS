namespace SimpleCQRS {
	public interface IMessageDispatcher {
		void Publish<TMessage>(IMessage message) where TMessage : IMessage;
	}
}
