using System.Security.Claims;
using Domain.Entities.Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;

namespace InternationalPaymentTransfer.ClaimsTransformation;

public class CustomClaimsTransformation: IClaimsTransformation
{
    private readonly IUnitOfWork _uow;

    public CustomClaimsTransformation(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userprofile = await _uow.AsyncRepository<UserProfile>().GetSingleBySpec(x => x.ApplicationUserId == userId);

        if (userprofile == null) return principal;

        ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;

        identity.AddClaim(new Claim("FirstName", userprofile.FirstName));
        identity.AddClaim(new Claim("LastName", userprofile.LastName));
        identity.AddClaim(new Claim("UserProfileId", userprofile.Id.ToString()));

        return principal;
    }
}