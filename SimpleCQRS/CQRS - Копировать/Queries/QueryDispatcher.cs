using SimpleCQRS.CQRS.Common;

namespace SimpleCQRS {
	public class QueryDispatcher : IQueryDispatcher {
		protected readonly IDispatcherComponentsResolver _componentsResolver;

		public QueryDispatcher(IDispatcherComponentsResolver componentsResolver) => _componentsResolver = componentsResolver;

		public TResult Dispatch<TParameter, TResult>(TParameter query)
			where TParameter : IQuery
			where TResult : IQueryResult {
			// Find the appropriate handler to call from those registered with Ninject based on the type parameters
			var handler = _componentsResolver.Get<IQueryHandler<TParameter, TResult>>();
			return handler.Retrieve(query);
		}
	}
}
