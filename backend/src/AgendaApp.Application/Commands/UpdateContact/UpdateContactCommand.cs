using MediatR;


namespace AgendaApp.Application.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
    }
}