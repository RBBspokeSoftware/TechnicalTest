namespace TechnicalTest.Data.Model;

public abstract class BaseUpdateAndDeleteModel<TIdType> : BaseUpdateModel<TIdType>, IModelDeleteInfo
{
    public int? DeletedByUserId { get; set; }
    public DateTime? DeleteDate { get; set; }
}