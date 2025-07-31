using Domain.Entities.User;

namespace Api.Extensions;

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string UserName => _accessor.HttpContext?.User.Identity?.Name ?? string.Empty;

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }
}