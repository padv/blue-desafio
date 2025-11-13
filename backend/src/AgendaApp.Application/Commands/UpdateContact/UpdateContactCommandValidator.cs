using FluentValidation;

namespace AgendaApp.Application.Commands.UpdateContact
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id é obrigatório");

            RuleFor(x => x.Nome)
                .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Nome)); // Só valida se foi preenchido

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email inválido")
                .When(x => !string.IsNullOrEmpty(x.Email)); // Só valida se foi preenchido

            RuleFor(x => x.Telefone)
                .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$")
                .WithMessage("Formato de telefone inválido. Use (XX) XXXXX-XXXX")
                .When(x => !string.IsNullOrEmpty(x.Telefone)); // Só valida se foi preenchido
        }
    }
}