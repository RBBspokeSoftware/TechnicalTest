namespace TechnicalTest.Data.Model;

public interface IModelDeleteInfo
{
    public int? DeletedByUserId { get; set;  }
    public DateTime? DeleteDate { get; set; }
}
    
public abstract class BaseDeleteModel<TIdType> : BaseCreateModel<TIdType>, IModelDeleteInfo
{
    public int? DeletedByUserId { get; set; }
    public DateTime? DeleteDate { get; set; }
}