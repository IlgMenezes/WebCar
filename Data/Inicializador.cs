using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCar.Data {
	public static class Inicializador {

		public static void AdicionarDadosTeste(WebCarContext context) {
			var veiculo1 = new Models.Veiculo {
				Marca = "WV",
				Modelo = "Gol",
				Ano = "2019"
			};
			var veiculo2 = new Models.Veiculo {
				Marca = "FIAT",
				Modelo = "Palio",
				Ano = "2020"
			};
			var veiculo3 = new Models.Veiculo {
				Marca = "GM",
				Modelo = "Onix",
				Ano = "2021"
			};
			var vendedor1 = new Models.Vendedor {
				Nome = "José da Silva",
				CPF = "74893773881",
				Email = "jose@email.com"
			};
			var vendedor2 = new Models.Vendedor {
				Nome = "Marcos Parreira",
				CPF = "23738812332",
				Email = "marcos@email.com"
			};
			context.Veiculos.Add(veiculo1);
			context.Veiculos.Add(veiculo2);
			context.Veiculos.Add(veiculo3);
			context.Vendedores.Add(vendedor1);
			context.Vendedores.Add(vendedor2);
			context.SaveChanges();
		}
	}
}
