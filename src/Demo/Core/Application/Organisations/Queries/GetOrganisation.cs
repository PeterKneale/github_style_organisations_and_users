using System.Data;
using Dapper;
using FluentValidation;

namespace Demo.Core.Application.Organisations.Queries;

public static class GetOrganisation
{
    public record Query(Guid OrganisationId) : IRequest<Result>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.OrganisationId).NotEmpty();
        }
    }

    public record Result(Guid Id, string Title, string? Description);

    private class Handler : IRequestHandler<Query, Result>
    {
        private readonly IDbConnection _db;

        public Handler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Result> Handle(Query query, CancellationToken token)
        {
            var id = query.OrganisationId;
            var sql = "select * from organisations where id = @id";
            var result = await _db.QuerySingleOrDefaultAsync<Result>(sql, new {id});
            if (result == null)
            {
                // todo: not found
                throw new NotImplementedException();
            }
            return result;
        }
    }
}