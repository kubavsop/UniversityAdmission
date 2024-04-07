namespace Admission.Domain.Common.Entities;

public abstract class BaseEntity: IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? DeleteTime { get; set; }
    public DateTime? ModifiedTime { get; set; }
}