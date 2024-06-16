
namespace Ports.In;

public class DtoAudit
{
    public string Id { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }
}