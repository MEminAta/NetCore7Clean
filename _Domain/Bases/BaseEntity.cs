using System.ComponentModel.DataAnnotations.Schema;
using Domain.Derived;

namespace Domain.Bases;

public class BaseEntity<T> : BaseEntity where T : struct
{
    public T Id { get; set; }
}

public class BaseEntity
{
    [ForeignKey(nameof(UpdateUser))] public int? UpdateUserId { get; set; }
    public User? UpdateUser { get; set; }
    public DateTime UpdateTime { get; set; }

    [ForeignKey(nameof(CreateUser))] public int? CreateUserId { get; set; }
    public User? CreateUser { get; set; }
    public DateTime CreateTime { get; set; }

    public bool IsDeleted { get; set; } = false;
}