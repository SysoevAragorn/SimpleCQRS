using System.Collections.Generic;

namespace SimpleCQRS.CQRS.Common {
	public interface IDispatcherComponentsResolver {
		IEnumerable<TComponent> GetAll<TComponent>() where TComponent : IDispatcherComponent;
		TComponent Get<TComponent>();
	}
}