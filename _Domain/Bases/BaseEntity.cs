using Domain.Entities;

namespace Domain.Bases;

public class BaseEntity
{
    public int? Id { get; set; }

    public int? UpdateByUserId { get; set; }
    public User? UpdateByUser { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? CreateByUserId { get; set; }
    public User? CreateByUser { get; set; }
    public DateTime? CreateTime { get; set; }
    public bool? IsActive { get; set; }
}