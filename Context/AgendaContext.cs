using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestandoApi.entities;

namespace TestandoApi.Context
{
    /// <summary>
    /// Classe de contexto do Entity Framework Core, responsável por gerenciar a conexão com o banco de dados.
    /// </summary>
    public class AgendaContext : DbContext
    {
        /// <summary>
        /// Construtor do contexto que recebe as opções de configuração (como a connection string).
        /// </summary>
        /// <param name="options">Opções de configuração do DbContext.</param>
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {
        }

        /// <summary>
        /// Representa a tabela de contatos no banco de dados.
        /// </summary>
        public DbSet<Contato> Contatos { get; set; }
    }
}
