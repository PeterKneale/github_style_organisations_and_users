using System.Data;
using Dapper;

namespace Demo.Core.Application.Organisations.Queries;

public static class ListMembersByOrganisation
{
    public record Query(Guid OrganisationId) : IRequest<IEnumerable<Result>>;

    public record Result(Guid Id, string Name, string Role);

    private class Handler : IRequestHandler<Query, IEnumerable<Result>>
    {
        private readonly IDbConnection _db;

        public Handler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Result>> Handle(Query message, CancellationToken token)
        {
            var organisationId = message.OrganisationId;
            var sql = "select u.id, CONCAT(first_name, ' ', last_name) AS name, m.role " +
                      "from organisation_member m join users u on m.member_id = u.id " +
                      "where organisation_id = @organisationId";
            return await _db.QueryAsync<Result>(sql, new {organisationId});
        }
    }
}