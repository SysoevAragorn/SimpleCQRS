using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBusCQRS;
using ServiceStack.Text;
using SimpleCQRS;

namespace EventBusCQRS.Common {
	public static class Extension {
		public static TMessage GetData<TMessage>(this IRabbitMessageHandler<TMessage> handler, byte[] body) where TMessage : IMessage {
			string jsonBody = Encoding.UTF8.GetString(body);
			return JsonSerializer.DeserializeFromString<TMessage>(jsonBody);
		}

		public static TCommand GetData<TCommand>(this IRabbitCommandHandler<TCommand> handler, byte[] body) where TCommand : ICommand {
			string jsonBody = Encoding.UTF8.GetString(body);
			return JsonSerializer.DeserializeFromString<TCommand>(jsonBody);
		}

		public static string GetGenericTypeName<TCommand>(this IRabbitCommandHandler<TCommand> handler) where TCommand : ICommand {
			foreach (Type intType in handler.GetType().GetInterfaces()) {
				if (intType.IsGenericType && intType.GetGenericTypeDefinition()
					== typeof(IRabbitCommandHandler<>)) {
					return intType.GetGenericArguments()[0].Name;
				}
			}
			throw new InvalidOperationException(string.Format("Handler {0} had not correct generic interface", handler.GetType().Name));
		}

		public static string GetGenericTypeName<TMessage>(IRabbitMessageHandler<TMessage> handler) where TMessage : IMessage {
			return handler.GetType().GetGenericArguments()[0].ToString();
		}
	}
}
