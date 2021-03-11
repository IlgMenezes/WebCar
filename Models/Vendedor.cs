using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCar.Models {

	[Serializable]
	public class Vendedor {
		[JsonIgnore]
		public int Id { get; set; }
		[JsonIgnore]
		public int VendaId { get; set; }
		public Venda Venda { get; set; }
		public String Nome { get; set; }
		public String CPF { get; set; }
		public String Email { get; set; }
	}
}
