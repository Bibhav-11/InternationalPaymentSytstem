using System.Security.Claims;
using Application.Interfaces;

namespace InternationalPaymentTransfer.Services;

public class CurrentUserService: ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("UserProfileId"));
        }
    }

    public string Email
    {
        get
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }
    }

    public string Fullname
    {
        get
        {
            return $"{_httpContextAccessor.HttpContext.User.FindFirstValue("FirstName")} {_httpContextAccessor.HttpContext.User.FindFirstValue("LastName")}";
        }
    }
}