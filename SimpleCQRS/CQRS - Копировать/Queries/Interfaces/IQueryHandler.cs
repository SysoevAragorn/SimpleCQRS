using SimpleCQRS.CQRS;

namespace SimpleCQRS {

	//TODO Ерализовать нормальный интерфейс из домашнего проекта
	public interface IQueryHandler<TParameter, TResult> : IDispatcherComponent
	   where TResult : IQueryResult
	   where TParameter : IQuery {
		TResult Retrieve(TParameter query);
	}
}
