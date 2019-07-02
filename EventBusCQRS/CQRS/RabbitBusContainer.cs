using System;
using System.Threading;
using System.Threading.Tasks;
using EventBusCQRS.ConnectionManager;
using log4net;
using RabbitMQ.Client.Events;

namespace EventBusCQRS.CQRS {
	public class RabbitBusContainer : IBusContainer {
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

		public RabbitBusContainer(IRabbitMQConnection rabbitConnection, IHandlerItem _handlerItem) {
			RabbitConnection = rabbitConnection;
			handlerItem = _handlerItem;
			BusEndpoint = _handlerItem.EndPoint;
			Log = LogManager.GetLogger("rabbit4netConsoleApp.Program");
		}

		public void Subscribe() {
			ExecutingTask = new Task(Execute);
			ExecutingTask.Start();
		}
		
		protected virtual void EventingHandler_Received(object sender, BasicDeliverEventArgs ea) {
			try {
				RabbitConnection.BasicAck(ea.DeliveryTag, false);

				byte[] body = ea.Body;
				handlerItem.Execute(ea.Body);
				if (handlerItem.IsNeedDispose) {
					Dispose();
				}

			} catch (Exception ex) {
				ReturnOnErorr(ea.Body);
				Log.Error(ex);
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
