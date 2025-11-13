using AgendaApp.Domain.Interfaces;
using MediatR;
using AgendaApp.Application.Exceptions;

namespace AgendaApp.Application.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Unit>
    {
        private readonly IContactRepository _repository;

        public UpdateContactCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(request.Id);
            if (contact == null)
                throw new KeyNotFoundException("Contato não encontrado");

            // Verifica se o email mudou e se outro contato já tem esse email
            if (!string.IsNullOrEmpty(request.Email) && contact.Email != request.Email)
            {
                var exists = await _repository.ExistsByEmailAsync(request.Email);
                if (exists)
                    throw new BusinessException("Email já cadastrado para outro contato");
            }

            // Só atualiza os campos que foram fornecidos
            var nome = !string.IsNullOrEmpty(request.Nome) ? request.Nome : contact.Nome;
            var email = !string.IsNullOrEmpty(request.Email) ? request.Email : contact.Email;
            var telefone = !string.IsNullOrEmpty(request.Telefone) ? request.Telefone : contact.Telefone;

            contact.Atualizar(nome, email, telefone);
            await _repository.UpdateAsync(contact);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}