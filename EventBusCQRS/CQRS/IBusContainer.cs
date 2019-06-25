using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusCQRS {
	public interface IBusContainer {
		Task ExecutingTask {
			get;
			set;
		}
		string BusEndpoint {
			get;
			set;
		}

		void Subscribe();
	}
}
