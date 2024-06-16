using Domain.Core.User;

namespace Domain.Core.Session;

public class SessionUser
{

    public UserId? Id { get; set; }
    
    public SessionId SessionId { get; set; }

    public string? FullName { get; set; }

    public string? Avatar { get; set; }
    
    public UserRole[] Roles { get; set; }

}