using Domain.Derived;

namespace Domain.Bases;

public class BaseEntity<T> : BaseEntity where T : struct
{
    public T Id { get; set; }
}

public class BaseEntity
{
    public int? UpdateByUserId { get; set; }
    public User? UpdateByUser { get; set; }

    public DateTime UpdateTime { get; set; }

    public int? CreateByUserId { get; set; }
    public User? CreateByUser { get; set; }
    public DateTime CreateTime { get; set; }
    public bool IsActive { get; set; }
}