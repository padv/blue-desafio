using AgendaApp.Application.DTOs;
using AutoMapper;
using MediatR;
using AgendaApp.Domain.Interfaces;

namespace AgendaApp.Application.Queries.SearchContacts
{
    public class SearchContactsQueryHandler : IRequestHandler<SearchContactsQuery, List<ContactDto>>
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public SearchContactsQueryHandler(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ContactDto>> Handle(SearchContactsQuery request, CancellationToken cancellationToken)
        {
            var term = request.Term?.Trim().ToLowerInvariant() ?? string.Empty;
            if (string.IsNullOrEmpty(term))
                return [];

            var contacts = await _repository.SearchAsync(term);
            return _mapper.Map<List<ContactDto>>(contacts);
        }
    }
}