using AgendaApp.Application.DTOs;
using AgendaApp.Application.Queries.GetContact.GetContactById;
using AgendaApp.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApp.Application.Queries.GetContact.GetContaById
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactDto>
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public GetContactByIdQueryHandler(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ContactDto> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(request.Id);
            if (contact == null)
                throw new KeyNotFoundException("Contato não encontrado");

            return _mapper.Map<ContactDto>(contact);
        }
    }
}