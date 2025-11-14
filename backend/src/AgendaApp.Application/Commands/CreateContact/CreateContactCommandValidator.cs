using FluentValidation;

namespace AgendaApp.Application.Commands.CreateContact
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório")
                .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$")
                .WithMessage("Formato de telefone inválido. Use (XX) XXXXX-XXXX");
        }
    }
}

