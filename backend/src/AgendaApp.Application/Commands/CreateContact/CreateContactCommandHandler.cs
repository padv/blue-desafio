using AgendaApp.Application.DTOs;
using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces;
using AutoMapper;
using MediatR;
using AgendaApp.Application.Exceptions;

namespace AgendaApp.Application.Commands.CreateContact
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactDto>
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public CreateContactCommandHandler(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ContactDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            // Verificar email duplicado
            if (await _repository.ExistsByEmailAsync(request.Email))
                throw new BusinessException("Email já cadastrado");

            var contact = Contact.Criar(request.Nome, request.Email, request.Telefone);
            await _repository.AddAsync(contact);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ContactDto>(contact);
        }
    }
}