using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCar.Models {
	[Serializable]
	public class Veiculo {
		[JsonIgnore]
		public int Id { get; set; }
		[JsonIgnore]
		public int VendaId { get; set; }
		public Venda Venda { get; set; }

		public String Marca { get; set; }
		public String Modelo { get; set; }
		public String Ano { get; set; }
	}
}
