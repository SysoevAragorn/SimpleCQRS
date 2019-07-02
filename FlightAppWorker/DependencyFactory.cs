using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSConsoleApp.Command;
using EventBusCQRS;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using EventBusCQRS.ConnectionManager;
using FlightAppWorker.CommandHandler;

namespace FlightAppWorker {
	public static class DependencyFactory {
		private static IKernel kernel = null;

		public static IKernel GetResolver(params INinjectModule[] modules) {
			if (kernel == null) {
				CreateKernel(modules);
			}
			return kernel;
		}

		private static void CreateKernel(params INinjectModule[] modules) {
			kernel = new StandardKernel(modules);
		}
		public static T GetNamedRealization<T>(string name, params IParameter[] parameters) {
			if (kernel == null) {
				throw new System.Exception("kernel not created");
			}
			return kernel.Get<T>(name, parameters);
		}

	}

	public class FlightModule : NinjectModule {
		public override void Load() {
			Bind<IRabbitCommandHandler<BookFlightCommand>>().To<BookFlightCommandHandler>();
			Bind<ICommonFactory>().To<CommonFactory>();
			Bind<IHandlerItem>().To<BookFlightCommandHandler>();
		}
	}
}
