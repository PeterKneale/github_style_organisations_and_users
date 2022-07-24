using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Demo.Core.Application.Users.Commands;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Auth;

public class Login : PageModel
{
    private readonly IMediator _mediator;
    private readonly ILogger<Login> _logs;

    public Login(IMediator mediator, ILogger<Login> logs)
    {
        _mediator = mediator;
        _logs = logs;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _mediator.Send(new Authenticate.Command(Email, Password));

        if (!result.Success)
        {
            _logs.LogWarning("Authentication was not successful: {Message}", result.Message);
            ModelState.AddModelError(string.Empty, result.Message!);
            return Page();
        }
        _logs.LogInformation("Authentication was successful: {Email}", result.User!.Email);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, result.User.Name.FullName),
            new(ClaimTypes.NameIdentifier, result.User.Id.Value.ToString()),
            new(ClaimTypes.Email, result.User.Email),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var authProperties = new AuthenticationProperties();
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

        return Redirect(returnUrl ?? "/");
    }

    [Display(Name = "Email")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = "Password")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}