using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBusCQRS;
using Ninject;
using Ninject.Parameters;

namespace FlightAppWorker {
	class Program {
		static void Main(string[] args) {
			var resolver = DependencyFactory.GetResolver(new FlightModule(), new BaseCQRSBindingModule());

			var handlerItems = resolver.GetAll<IHandlerItem>().ToList();
			foreach (var item in handlerItems) {
				var commandParams = new ConstructorArgument[] {
					new ConstructorArgument("_handlerItem", item)
				};
				var busContainer = resolver.Get<IBusContainer>(commandParams);
				busContainer.Subscribe();
			}
		}
	}
}
