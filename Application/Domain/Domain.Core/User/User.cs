using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Core.User;


[Table("Users")]
public class User : EntityAudit
{
    public User(
        string fullName,
        string displayName,
        UserRole role,
        string email,
        string phoneNumber,
        string avatar, 
        DateTime birthDate
    )
    {
        FullName = fullName;
        DisplayName = displayName;
        Role = role;
        Email = email;
        PhoneNumber = phoneNumber;
        Avatar = avatar;
        BirthDate = birthDate;
    }

    // Bắt buộc. Luôn phải có empty constructor cho EF mapper 
    public User()
    {
    }

    public string FullName { get; set; }

    public string? DisplayName { get; set; }

    public UserRole Role { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Avatar { get; set; }
    
    public DateTime? BirthDate { get; set; }

    public ICollection<UserPassword> Passwords { get; set; } = new List<UserPassword>();
}