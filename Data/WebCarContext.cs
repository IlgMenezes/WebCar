using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCar.Models;

namespace WebCar.Data {
	public class WebCarContext : DbContext {
        public WebCarContext(DbContextOptions<WebCarContext> options)
            : base(options) {
        }

        public DbSet<Vendedor> Vendedores { get; set; }

        public DbSet<Venda> Vendas { get; set; }

        public DbSet<Veiculo> Veiculos { get; set; }
    }
}
