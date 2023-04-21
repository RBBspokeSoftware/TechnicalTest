namespace TechnicalTest.Data.Model;

public interface IModelCreateInfo
{
    public int CreatedByUserId { get; set;  }
    public DateTime CreateDate { get; set; }
}

public class BaseCreateModel<T> : BaseModelKey<T>, IModelCreateInfo
{
    public int CreatedByUserId { get; set;  }
    public DateTime CreateDate { get; set; }
}