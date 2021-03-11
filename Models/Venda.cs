using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebCar.Models {
	[Serializable]
	public class Venda {
		[JsonIgnore]
		public int Id { get; set; }
		public DateTime Data { get; set; }
		public Vendedor Vendedor { get; set; }
		public List<Veiculo> Veiculos { get; set; }
		[JsonProperty("StatusVenda")]
		public StatusVenda Status { get; set; }
	}

	[JsonConverter(typeof(CustomStringToEnumConverter))]
	public enum StatusVenda {
		[Description("Confirmação de Pagamento")]
		ConfirmacaoPagamento,

		[Description("Pagamento Aprovado")]
		PagamentoAprovado,

		[Description("Em Transporte")]
		EmTransporte,

		[Description("Entregue")]
		Entregue,

		[Description("Cancelada")]
		Cancelada

	}

}
public class CustomStringToEnumConverter : StringEnumConverter {
	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
		if (string.IsNullOrEmpty(reader.Value?.ToString())) {
			return null;
		}

		object parsedEnumValue;

		var isValidEnumValue = Enum.TryParse(objectType.GenericTypeArguments[0], reader.Value.ToString(), true, out parsedEnumValue);

		if (isValidEnumValue) {
			return parsedEnumValue;
		}
		else {
			return null;
		}
	}
}