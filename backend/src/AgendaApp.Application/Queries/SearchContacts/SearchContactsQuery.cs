using AgendaApp.Application.DTOs;
using MediatR;


namespace AgendaApp.Application.Queries.SearchContacts
{
    public class SearchContactsQuery : IRequest<List<ContactDto>>
    {

        public string? Term { get; set; } // termo de pesquisa, pode ser nome ou email
    }
}