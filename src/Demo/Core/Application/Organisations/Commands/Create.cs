using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Organisations;
using FluentValidation;

namespace Demo.Core.Application.Organisations.Commands;

public static class Create
{
    public record Command(Guid OrganisationId, string Title, string? Description) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.OrganisationId).NotEmpty();
            RuleFor(m => m.Title).NotEmpty().MaximumLength(50);
            RuleFor(m => m.Description).MaximumLength(50);
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IOrganisationRepository _organisations;
        private readonly ICurrentContext _context;

        public Handler(IOrganisationRepository organisations, ICurrentContext context)
        {
            _organisations = organisations;
            _context = context;
        }

        public async Task<Unit> Handle(Command command, CancellationToken token)
        {
            var id = OrganisationId.CreateInstance(command.OrganisationId);
            var title = new OrganisationTitle(command.Title, command.Description);

            var organisation = Organisation.CreateInstance(id, title, _context.CurrentUserId);

            await _organisations.Add(organisation);

            return Unit.Value;
        }
    }
}