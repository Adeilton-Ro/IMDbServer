using IMDb.Application;
using IMDb.Application.Services.UserInfo;
using System.Security.Claims;

namespace IMDbServer.Api.RequestHandling;
public class RequestUserInfoService : IUserInfoService
{
	private readonly IHttpContextAccessor httpContextAccessor;
	private IEnumerable<Claim>? claimsFields;
	private IEnumerable<Claim>? Claims => claimsFields ??= httpContextAccessor.HttpContext?.User.Claims;

	public RequestUserInfoService(IHttpContextAccessor httpContextAccessor)
	{
		this.httpContextAccessor = httpContextAccessor;
    }
	private Guid? idField;
	public Guid? Id => idField ??= Utils.TryParseNullSafe(Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
}