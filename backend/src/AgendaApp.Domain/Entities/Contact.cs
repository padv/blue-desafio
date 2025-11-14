

namespace AgendaApp.Domain.Entities
{
    public class Contact
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataAtualizacao { get; private set; }
        public bool Ativo { get; private set; }

        // Construtor privado
        private Contact()
        {
            Nome = string.Empty;
            Email = string.Empty;
            Telefone = string.Empty;
        }

        public static Contact Criar(string nome, string email, string telefone)
        {
            return new Contact
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                Email = email,
                Telefone = telefone,
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow,
                Ativo = true
            };
        }

        public void Atualizar(string nome, string email, string telefone)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Desativar()
        {
            Ativo = false;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}