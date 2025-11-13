using AgendaApp.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace AgendaApp.Application.Commands.CreateContact
{
    public class CreateContactCommand : IRequest<ContactDto>
    {   [Required]
        public required string Nome { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Telefone { get; set; }
    }
}