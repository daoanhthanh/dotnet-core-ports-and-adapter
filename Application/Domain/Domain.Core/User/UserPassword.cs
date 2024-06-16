using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Core.User;

[Table("Passwords")]
public class UserPassword
{
    public long Id { get; set; }

    public string Value { get; set; }

    public DateTime CreatedAt { get; set; }

    public EntityId UserId { get; set; }
}