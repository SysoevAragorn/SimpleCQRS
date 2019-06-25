using System;
using System.Threading;
using System.Threading.Tasks;
using EventBusCQRS.ConnectionManager;
using RabbitMQ.Client.Events;

namespace EventBusCQRS.CQRS {
	public class RabbitBusContainer : IBusContainer {
		//protected readonly IMessageDispatcher messageDispatcher;
		protected readonly IHandlerItem handlerItem;


		#region Field: Public

		public int timeOut = 10000;
		public EventingBasicConsumer EventingConsumer;
		public IRabbitMQConnection RabbitConnection;
		public static ILog Log;
		#endregion

		public Task ExecutingTask {
			get;
			set;
		}
		public string BusEndpoint {
			get;
			set;
		}

		public RabbitBusContainer(IRabbitMQConnection rabbitConnection, /*IMessageDispatcher _messageDispatcher,*/ IHandlerItem _handlerItem) {
			RabbitConnection = rabbitConnection;
			//messageDispatcher = _messageDispatcher;
			handlerItem = _handlerItem;
			BusEndpoint = _handlerItem.EndPoint;
		}

		public void Subscribe() {
			ExecutingTask = new Task(Execute);
			ExecutingTask.Start();
		}


		public virtual void EventingHandler_Received(object sender, BasicDeliverEventArgs ea) {
			try {
				RabbitConnection.BasicAck(ea.DeliveryTag, false);

				byte[] body = ea.Body;
				handlerItem.Execute(ea.Body);
				if (handlerItem.IsNeedDispose) {
					Dispose();
				}

			} catch (Exception ex) {
				ReturnOnErorr(ea.Body);
				messageDispatcher.Publish<ErrorMessage>(new ErrorMessage(ex));
			}
		}

		protected virtual void ReturnOnErorr(byte[] data) {
			RabbitConnection.PublishToQueue(BusEndpoint, data);
			Thread.Sleep(timeOut);
		}

		public virtual void Execute() {
			RabbitConnection.QueueDeclare(BusEndpoint, true, false, false);
			EventingConsumer = RabbitConnection.CreateEventingBasicConsumer(BusEndpoint, 0, 1);
			EventingConsumer.Received += EventingHandler_Received;
		}

		public virtual void Dispose() {
			EventingConsumer.Received -= EventingHandler_Received;
			RabbitConnection.QueueDelete(BusEndpoint, false, false);
			EventingConsumer.Model.Close();
			EventingConsumer.Model.Dispose();
			RabbitConnection.Dispose();
		}

	}
}
