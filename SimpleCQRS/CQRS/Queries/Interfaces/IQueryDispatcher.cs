namespace SimpleCQRS {
	//public interface IQueryDispatcher
	//{
	//	TResult Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
	//}

	public interface IQueryDispatcher {
		TResult Dispatch<TParameter, TResult>(TParameter query)
			where TParameter : IQuery
			where TResult : IQueryResult;

	}



}
