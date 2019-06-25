using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusCQRS {
	public interface IHandlerItem {
		/// <summary>
		/// Execute handler logic
		/// </summary>
		/// <param name="data"> Data from Queue</param>
		void Execute(byte[] data);

		string EndPoint {
			get;
			set;
		}
		bool IsNeedDispose {
			get;
			set;
		}


	}
}
