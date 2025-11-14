using AgendaApp.Domain.Interfaces;
using MediatR;
using AgendaApp.Application.Exceptions;
using AutoMapper;
using AgendaApp.Application.DTOs;

namespace AgendaApp.Application.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ContactDto>
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public UpdateContactCommandHandler(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ContactDto> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
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

            return _mapper.Map<ContactDto>(contact);
        }
    }
}