using System;
using MarcoCarvalho.Capacitacao.Biblioteca.Repositories;

namespace MarcoCarvalho.Capacitacao.Biblioteca.Models
{
    public class Locacao
    {
        private LivroRepository livroRepository = new LivroRepository();
        private ClienteRepository clienteRepository = new ClienteRepository();

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdLivro { get; set; }
        public DateTime Retirada { get; set; }
        public DateTime Devolucao { get; set; }
        
        public Cliente Cliente
        {
            get { return this.clienteRepository.GetClienteById(this.IdCliente); }
        }

        public Livro Livro
        {
            get { return this.livroRepository.GetLivroById(this.IdLivro); }
        }
    }
}