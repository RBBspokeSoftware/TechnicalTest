namespace TechnicalTest.Data.Model;

public interface IModelKey<T>
{
    public T Id { get; set; }
}

public abstract class BaseModelKey<T> : IModelKey<T>
{
    public T Id { get; set; }
}