
using System.Collections.Generic;
using Ninject;

namespace SimpleCQRS.CQRS.Common {
	public class NinjectComponentsResolver : IDispatcherComponentsResolver {
		protected IKernel _kernel;

		public NinjectComponentsResolver(IKernel kernel) => _kernel = kernel;

		public TComponent Get<TComponent>() => _kernel.Get<TComponent>();

		public IEnumerable<TComponent> GetAll<TComponent>() where TComponent : IDispatcherComponent => _kernel.GetAll<TComponent>();
	}
}
