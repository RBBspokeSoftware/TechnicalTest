namespace TechnicalTest.Data.Model;

public interface IModelUpdateInfo
{
    public int? UpdatedByUserId { get; set;  }
    public DateTime? UpdateDate { get; set; }
}

public abstract class BaseUpdateModel<TIdType> : BaseCreateModel<TIdType>, IModelUpdateInfo
{
    public int? UpdatedByUserId { get; set; }
    public DateTime? UpdateDate { get; set; }
}