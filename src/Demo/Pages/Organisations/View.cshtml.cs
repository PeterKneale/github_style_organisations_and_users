using System.ComponentModel.DataAnnotations;
using Demo.Core.Application.Organisations.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Organisations;

public class View : PageModel
{
    private readonly IMediator _mediator;

    public View(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Organisation = await _mediator.Send(new GetOrganisation.Query(id));
        Members = await _mediator.Send(new ListMembersByOrganisation.Query(id));
        return Page();
    }
    
    public GetOrganisation.Result Organisation { get; private set; }
    
    public IEnumerable<ListMembersByOrganisation.Result> Members { get; private set; }
}