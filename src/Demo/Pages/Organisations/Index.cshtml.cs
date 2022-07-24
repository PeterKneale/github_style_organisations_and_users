using Demo.Core.Application.Organisations.Queries;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Organisations;

public class Index : PageModel
{
    private readonly IMediator _mediator;

    public Index(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnGetAsync()
    {
        Data = await _mediator.Send(new ListOrganisations.Query());
    }

    public IEnumerable<ListOrganisations.Result> Data { get; private set; }
}