using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestandoApi.Context;
using TestandoApi.entities;

namespace TestandoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        // Injeção do contexto do banco de dados (AgendaContext) via construtor
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo contato.
        /// </summary>
        /// <parametro name="contato">Objeto contato a ser adicionado no banco de dados.</parametro>
        /// <returns>Retorna o contato criado com status 200 (OK).</returns>
        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return Ok(contato);
        }

        // criando o select da API, e verificando caso seja o id nulo
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
            {
                return NotFound();
            }
            return Ok(contato);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if (contatoBanco == null)
            {
                return NotFound();
            }

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return Ok(contatoBanco);
        }

        [HttpDelete("{id}")]
        
        public ActionResult Deletar(int id)
        {
            var contatoBanco = _context.Contatos.Find(id);
            if (contatoBanco == null)
            {
                return NotFound();
            }
            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
