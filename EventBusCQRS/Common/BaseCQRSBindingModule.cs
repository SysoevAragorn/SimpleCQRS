using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBusCQRS.ConnectionManager;
using EventBusCQRS.CQRS;
using Ninject.Modules;
using SimpleCQRS;
using SimpleCQRS.CQRS.Common;

namespace EventBusCQRS {
	public class BaseCQRSBindingModule : NinjectModule {
		public override void Load() {
			Bind<IDispatcherComponentsResolver>().To<NinjectComponentsResolver>();

			Bind<ICommandDispatcher>().To<RabbitCommandDispatcher>();
			Bind<IMessageDispatcher>().To<MessageDispatcher>();
			Bind<IBusContainer>().To<RabbitBusContainer>();
			Bind<IRabbitMQConnection>().To<RabbitMQConnection>();

		}
	}
}
