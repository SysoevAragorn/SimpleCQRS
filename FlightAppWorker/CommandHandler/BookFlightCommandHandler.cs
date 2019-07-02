using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSConsoleApp.Command;
using EventBusCQRS;
using SimpleCQRS;
using EventBusCQRS.Common;

namespace FlightAppWorker.CommandHandler {
	public class BookFlightCommandHandler : IRabbitCommandHandler<BookFlightCommand> {
		protected readonly IMessageDispatcher messageDispatcher;

		public string EndPoint {
			get;
			set;
		}
		public bool IsNeedDispose {
			get;
			set;
		}

		public BookFlightCommandHandler(IMessageDispatcher _messageDispatcher) {
			messageDispatcher = _messageDispatcher;
			EndPoint = this.GetGenericTypeName();
		}

		public void Handle(BookFlightCommand command) {
			List<Type> genTypes = new List<Type>();
			foreach (Type intType in this.GetType().GetInterfaces()) {
				if (intType.IsGenericType && intType.GetGenericTypeDefinition()
					== typeof(IRabbitCommandHandler<>)) {
					genTypes.Add(intType.GetGenericArguments()[0]);
				}
			}
		}


		public void Execute(byte[] data) {
			var command = this.GetData(data);
			Handle(command);
		}
	}
}
