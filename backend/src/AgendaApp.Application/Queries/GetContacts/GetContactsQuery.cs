using AgendaApp.Application.DTOs;
using MediatR;


namespace AgendaApp.Application.Queries.GetContacts
{
    public class GetContactsQuery : IRequest<List<ContactDto>>
    {
        
    }
}