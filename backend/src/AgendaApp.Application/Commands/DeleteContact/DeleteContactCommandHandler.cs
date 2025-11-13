
using AgendaApp.Domain.Interfaces;
using MediatR;

namespace AgendaApp.Application.Commands.DeleteContact
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Unit>
    {
        private readonly IContactRepository _repository;

        public DeleteContactCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(request.Id);
            if (contact == null)
                throw new KeyNotFoundException("Contato não encontrado");

            contact.Desativar(); // não deleta de fato, e sim desativa do banco de dados
            await _repository.UpdateAsync(contact);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}