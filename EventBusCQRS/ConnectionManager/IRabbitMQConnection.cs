using System;
using RabbitMQ.Client.Events;

namespace EventBusCQRS.ConnectionManager {
	public interface IRabbitMQConnection : IDisposable {


		#region Methods: Public


		/// <summary>
		/// Возвращает EventingBasicConsumer
		/// </summary>
		/// <param name="queueName">Имя очереди</param>
		/// <param name="prefetchSize">Резервируемый размер</param>
		/// <param name="prefetchCount">Резервируемое кол-во</param>
		/// <returns>EventingBasicConsumer</returns>
		EventingBasicConsumer CreateEventingBasicConsumer(string queuename,
					ushort prefetchsize, ushort prefetchcount);

		/// <summary>
		/// Публикация в очередь
		/// </summary>
		/// <param name="queueName">Имя очереди</param>
		/// <param name="data">Объект данных</param>
		void PublishToQueue(string queueName, object data);

		/// <summary>
		/// Публикация в очередь
		/// </summary>
		/// <param name="queueName">Имя очереди</param>
		/// <param name="message">Сереализированный объект</param>
		void PublishToQueue(string queueName, string message);

		/// <summary>
		/// Публикация в очередь
		/// </summary>
		/// <param name="queueName">Имя очереди</param>
		/// <param name="body">Сереализированный в байты объект</param>
		void PublishToQueue(string queueName, byte[] body);

		/// <summary>
		/// Публикация в Exchange
		/// </summary>
		/// <param name="exchangeName">Имя Exchange</param>
		/// <param name="routingKey">RoutingKey</param>
		/// <param name="message">Сереализированный объект</param>
		void PublishToExchange(string exchangeName, string routingKey, string message);
		void BasicAck(ulong deliveryTag, bool multiply);

		

		/// <summary>
		/// Публикация в Exchange
		/// </summary>
		/// <param name="exchangeName">Имя Exchange</param>
		/// <param name="routingKey">RoutingKey</param>
		/// <param name="data">Объект данных</param>
		void PublishToExchange(string exchangeName, string routingKey, object data);

		/// <summary>
		/// Declare exchange and bind it to queue.
		/// </summary>
		void DeclareDelayExchange(string exchangeName);

		void QueueDeclare(string queueName, bool durable, bool exclusive, bool autoDelete);

		/// <summary>
		/// Publishes delayed message to queue.
		/// </summary>
		/// <param name="message">Message to send to queue.</param>
		/// <param name="delay">Delay(in milliseconds) before sending message.</param>
		/// <param name="headers">Collection of message headers.</param>
		void PublishDelayedMessage(string message, int delay, string exchangeName, string routingKey);
		void QueueDelete(string busEndpoint, bool ifUnused, bool ifEmpty);

	}
	#endregion

}
