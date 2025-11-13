using AgendaApp.Application.DTOs;
using MediatR;


namespace AgendaApp.Application.Queries.GetContact.GetContactById
{
    public class GetContactByIdQuery : IRequest<ContactDto>
    {
        public Guid Id { get; set; }
    }
}