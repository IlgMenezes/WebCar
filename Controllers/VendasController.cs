using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCar.Data;
using WebCar.Models;

namespace WebCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly WebCarContext _context;

        public VendasController(WebCarContext context)
        {
            _context = context;
        }


        // GET: api/Vendas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenda([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var venda = await _context.Vendas.Include(v => v.Vendedor).Include(v=>v.Veiculos).FirstOrDefaultAsync(v => v.Id == id);

            if (venda == null)
            {
                return NotFound();
            }

            return Ok(venda);
        }

        // PUT: api/Vendas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenda([FromRoute] int id, [FromBody] DadosAtualizacao dadosAtualizacao)
        {
            var venda = await _context.Vendas.Include(v => v.Vendedor).Include(v=>v.Veiculos).FirstOrDefaultAsync(v => v.Id == id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venda.Id)
            {
                return BadRequest();
            }
            if(ValidarModificacaoStatus((StatusVenda)dadosAtualizacao.statusVenda, venda.Status)){
                venda.Status = (StatusVenda)dadosAtualizacao.statusVenda;
            }
            else{
                return BadRequest("Modificação Inválida!");
            }

            _context.Entry(venda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(venda);
        }

        private bool ValidarModificacaoStatus(StatusVenda statusVendaNovo, StatusVenda statusVendaAntigo) {
            bool retorno = false;
            switch (statusVendaNovo) {
                case StatusVenda.Cancelada: {
                    if(statusVendaAntigo == StatusVenda.ConfirmacaoPagamento || statusVendaAntigo == StatusVenda.PagamentoAprovado){
                        retorno = true;
                    }
                    break;
                }
                case StatusVenda.EmTransporte: {
                    if (statusVendaAntigo == StatusVenda.PagamentoAprovado) {
                        retorno = true;
                    }
                    break;
                }
                case StatusVenda.Entregue: {
                    if (statusVendaAntigo == StatusVenda.EmTransporte) {
                        retorno = true;
                    }
                    break;
                }
                case StatusVenda.PagamentoAprovado: {
                    if (statusVendaAntigo == StatusVenda.ConfirmacaoPagamento) {
                        retorno = true;
                    }
                    break;
                }
            }
            return retorno;
        }

        // POST: api/Vendas
        [HttpPost("gravar")]
        public async Task<IActionResult> PostVenda([FromBody] DadosVenda dadosVenda)
        {
            try {
                Venda Venda = new Venda() {
                    Data = DateTime.Now,
                    Status = StatusVenda.ConfirmacaoPagamento,
                    Veiculos = RetornarVeiculos(dadosVenda.veiculos),
                    Vendedor = _context.Vendedores.Find(dadosVenda.CodVendedor)
                };

                _context.Add(Venda);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetVenda", new { id = Venda.Id }, Venda);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        private List<Veiculo> RetornarVeiculos(int[] codVeiculos) {
            List<Veiculo> veiculos = new List<Veiculo>();
            foreach (int item in codVeiculos) {
                var veiculo = _context.Veiculos.Find(item);
                if (veiculo.VendaId != 0) {
                    throw new Exception("Carro vendido ou em processo de venda!");
                }
                else{
                    veiculos.Add(veiculo);
                }

            }
            return veiculos;
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.Id == id);
        }
    }
    public struct DadosAtualizacao {
        public int statusVenda;
    }
    public struct DadosVenda {
        public int CodVendedor;
        public int[] veiculos;
    }
}