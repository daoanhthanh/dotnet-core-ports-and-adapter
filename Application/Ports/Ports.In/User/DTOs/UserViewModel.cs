using Domain.Core.User;

namespace Ports.In.User.DTOs;

public class UserViewModel : DtoAudit
{
    public string FullName { get; set; }

    public string DisplayName { get; set; }

    public UserRole Role { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Avatar { get; set; }

    public DateTime BirthDate { get; set; }
}
