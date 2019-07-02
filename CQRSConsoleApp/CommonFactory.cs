using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBusCQRS.ConnectionManager;

namespace FlightConsoleApp {
	public class CommonFactory : ICommonFactory {
		public string GetRMQConnection() => ConfigurationManager.ConnectionStrings["RabbitMQ"].ConnectionString;
	}
}
