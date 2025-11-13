using AgendaApp.Application.DTOs;
using AgendaApp.Domain.Interfaces;
using AutoMapper;
using MediatR;


namespace AgendaApp.Application.Queries.GetContacts
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, List<ContactDto>>
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public GetContactsQueryHandler(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _repository.GetAllAsync();
            return _mapper.Map<List<ContactDto>>(contacts);
        }
    }
}