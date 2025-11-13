using MediatR;

namespace AgendaApp.Application.Commands.DeleteContact
{
    public class DeleteContactCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}