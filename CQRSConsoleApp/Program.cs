using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSConsoleApp.Command;
using EventBusCQRS;
using Ninject;
using SimpleCQRS;

namespace CQRSConsoleApp {
	class Program {
		static void Main(string[] args) {
			var resolver = DependencyFactory.GetResolver(new FlightModule(), new BaseCQRSBindingModule());
			var dispatcher = resolver.Get<ICommandDispatcher>();
			dispatcher.Execute(new BookFlightCommand());
		}
	}
}
